using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

/// <summary>
///  EnemyManager is used to track every enemy in the current level, it controls the spawning/despawning and life cycle of every enemy
/// </summary>
public class EnemyManager : MonoBehaviour
{
    private const int MAX_MONSTER_WEIGHT = 300;
    private const int DEFAULT_POOL_SIZE = 50;
    private const float MINIMUM_SPAWN_DISTANCE = 15.0F;
    private const float MAXIMUM_SPAWN_DISTANCE = 25.0F;
    [SerializeField] private List<WeightedEnemy> currentEnemyPool;
    [SerializeField] private Dictionary<string, IObjectPool<GameObject>> currentEnemyPoolObjectPool;
    [SerializeField] private List<Enemy> currentEnemyList;
    private int currentMonsterWeight = 0;

    private void Awake()
    {
        currentEnemyPoolObjectPool = new Dictionary<string, IObjectPool<GameObject>>();
    }

    private GameObject temporaryCurrentEnemyGameObjectUsedForSettingUpObjectPool;

    public void SetNewEnemyPool(List<WeightedEnemy> newEnemyPool)
    {
        currentEnemyPool = newEnemyPool;
        Debug.Log("SET NEW ENEMY POOL");

        foreach (WeightedEnemy enemy in currentEnemyPool)
        {
            temporaryCurrentEnemyGameObjectUsedForSettingUpObjectPool = enemy.prefab;
            IObjectPool<GameObject> newObjectPool = new ObjectPool<GameObject>(
                CreatePool, 
                OnGetFromPool, 
                OnReleaseToPool, 
                OnDestroyPoolObject, 
                true, 
                DEFAULT_POOL_SIZE, 
                MAX_MONSTER_WEIGHT);

            if (!currentEnemyPoolObjectPool.ContainsKey(enemy.prefab.name)) {
                currentEnemyPoolObjectPool.Add(enemy.prefab.name, newObjectPool);
            }

        }
    }

    #region

    private int count = 0; 
    private GameObject CreatePool()
    {
        GameObject newGameObject = Instantiate(temporaryCurrentEnemyGameObjectUsedForSettingUpObjectPool, Vector3.zero, Quaternion.identity);

        if (newGameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            // enemyComponent.SetPool = currentEnemyPoolObjectPool[enemyComponent.name];
        }

        return newGameObject;
    }

    private void OnGetFromPool(GameObject poolObject)
    {
        Debug.Log("GET");
        poolObject.SetActive(true);
    }

    private void OnReleaseToPool(GameObject poolObject)
    {
        poolObject.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject poolObject)
    {
        Destroy(poolObject);
    } 
    #endregion



    private WeightedEnemy PickMonster()
    {
        int random = Random.Range(0, 100);
        float accumulation = 0;

        foreach (WeightedEnemy enemy in currentEnemyPool)
        {
            if (random < (accumulation + enemy.spawnChance))
            {
                return enemy;
            }

            accumulation += enemy.spawnChance;
        }

        return currentEnemyPool[0];
    }

    private Vector3 PickSpawnLocation()
    {
        float randomDistance = Random.Range(MINIMUM_SPAWN_DISTANCE, MAXIMUM_SPAWN_DISTANCE);
        float randomAngle = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;

        Vector3 spawnDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);
        Vector3 spawnPosition = GameManager.Instance.playerManager.currentPlayer.transform.position + (spawnDirection * randomDistance);

        return spawnPosition;
    }

    public void ExecuteSpawnCall()
    {
        if (currentMonsterWeight >= MAX_MONSTER_WEIGHT)
        {
            return;
        }

        WeightedEnemy monster = PickMonster();
        Vector3 spawnPosition = PickSpawnLocation();

        GameObject spawnedEnemy = currentEnemyPoolObjectPool[monster.prefab.name].Get();

        if (spawnedEnemy == null)
        {
            return;
        }

        spawnedEnemy.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);

        // spawnedEnemy.ResetStat();
        
        if (spawnedEnemy.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            currentEnemyList.Add(enemyComponent);
        }

        currentMonsterWeight += monster.spawnWeight;
    }
}
