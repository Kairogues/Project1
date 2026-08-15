using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This class is the composition way of enabling OnSpawn and OnDespawn in the object pool pattern instead of inheritence
/// </summary>
[DisallowMultipleComponent]
public class PooledObject : MonoBehaviour
{
    private IObjectPool<GameObject> originPool;
    private IPoolable[] poolablesInChildren;

    private void Awake()
    {
        poolablesInChildren = GetComponentsInChildren<IPoolable>(true);
    }

    public IObjectPool<GameObject> GetOriginPool()
    {
        return originPool;
    }

    public void SetOriginPool(IObjectPool<GameObject> pool)
    {
        originPool = pool;
    }

    public void TriggerSpawn()
    {
        for (int i = 0; i < poolablesInChildren.Length; i++)
        {
            poolablesInChildren[i].OnSpawn();
        }
    }

    public void TriggerDespawn()
    {
        for (int i = 0; i < poolablesInChildren.Length; i++)
        {
            poolablesInChildren[i].OnDespawn();
        }
    }

    public void ReleaseToPool()
    {
        if (originPool != null)
        {
            originPool.Release(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}