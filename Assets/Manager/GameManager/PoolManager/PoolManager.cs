using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private int defaultCapacity = 40;
    [SerializeField] private int maxPoolSize = 300;

    // Map the object to its pool
    private Dictionary<GameObject, IObjectPool<GameObject>> pools = new();

    // Map the object to its PooledObject component, so it will not have to GetComponen()t the PooledObject everytime
    private Dictionary<GameObject, PooledObject> instanceMap = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #region Spawn API

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (prefab == null) 
        {
            return null;
        }

        IObjectPool<GameObject> pool = GetOrCreatePool(prefab);
        GameObject instance = pool.Get();

        instance.transform.SetPositionAndRotation(position, rotation);
        if (parent != null)
        {
            instance.transform.SetParent(parent);
        }

        return instance;
    }

    public T Spawn<T>(T componentPrefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
    {
        GameObject spawingObject = Spawn(componentPrefab.gameObject, position, rotation, parent);

        if (spawingObject != null)
        {
            return spawingObject.GetComponent<T>();
        }

        return null;
    }

    #endregion

    #region Release API

    public void Release(GameObject instance)
    {
        if (instance == null) return;

        if (instanceMap.TryGetValue(instance, out PooledObject pooledObj))
        {
            pooledObj.ReleaseToPool();
        }
        else
        {
            Destroy(instance);
        }
    }

    public void Release<T>(T componentInstance) where T : Component
    {
        if (componentInstance != null) 
        {
            Release(componentInstance.gameObject);
        }
    }

    #endregion

    #region Internal Pool Factory

    private IObjectPool<GameObject> GetOrCreatePool(GameObject prefab)
    {
        if (pools.TryGetValue(prefab, out IObjectPool<GameObject> existingPool))
        {
            return existingPool;
        }

        IObjectPool<GameObject> newPool = null;

        newPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject instance = Instantiate(prefab);

                if (!instance.TryGetComponent(out PooledObject pooledObj))
                {
                    pooledObj = instance.AddComponent<PooledObject>();
                }

                pooledObj.SetOriginPool(newPool);
                instanceMap[instance] = pooledObj;
                return instance;
            },
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPoolObject,
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxPoolSize
        );

        pools.Add(prefab, newPool);
        return newPool;
    }

    private void OnGetFromPool(GameObject pooledObject)
    {
        pooledObject.SetActive(true);
        if (instanceMap.TryGetValue(pooledObject, out PooledObject pooledObj))
        {
            pooledObj.TriggerSpawn();
        }
    }

    private void OnReleaseToPool(GameObject pooledObject)
    {
        if (instanceMap.TryGetValue(pooledObject, out PooledObject pooledObj))
        {
            pooledObj.TriggerDespawn();
        }

        pooledObject.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject pooledObject)
    {
        instanceMap.Remove(pooledObject);
        Destroy(pooledObject);
    }

    #endregion
}