using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

/// <summary>
///  EntityManager is used to track every enemy in the current level, it controls the spawning/despawning and life cycle of every enemy
/// </summary>
public class EntityManager : MonoBehaviour
{
    private const int MAX_MONSTER_WEIGHT = 300;
    private const int DEFAULT_POOL_SIZE = 50;
    private const float MINIMUM_SPAWN_DISTANCE = 15.0F;
    private const float MAXIMUM_SPAWN_DISTANCE = 25.0F;
    [SerializeField] private List<WeightedEnemy> currentEnemyPool;
    [SerializeField] private Dictionary<string, IObjectPool<Entity>> currentEntityObjectPool;
    [SerializeField] private List<Enemy> currentEnemyList;
    [SerializeField] private List<Projectile> currentProjectileList;
    private int currentMonsterWeight = 0;

    private void Awake()
    {
        currentEntityObjectPool = new Dictionary<string, IObjectPool<Entity>>();
    }

    public void SetNewEnemyPool(List<WeightedEnemy> newEnemyPool)
    {
        currentEnemyPool = newEnemyPool;
        Debug.Log("SET NEW ENEMY POOL");

        foreach (WeightedEnemy enemy in currentEnemyPool)
        {
            Entity prefabToSpawn = enemy.prefab;
            string poolKey = prefabToSpawn.name;

            IObjectPool<Entity> newObjectPool = null;

            newObjectPool = new ObjectPool<Entity>(
                createFunc: () =>
                {
                    Entity newEntity = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity, transform);
                    newEntity.SetPool(newObjectPool);

                    return newEntity;
                }, 
                OnGetFromPool, 
                OnReleaseToPool, 
                OnDestroyPoolObject, 
                true, 
                DEFAULT_POOL_SIZE, 
                MAX_MONSTER_WEIGHT);

            if (!currentEntityObjectPool.ContainsKey(poolKey)) {
                currentEntityObjectPool.Add(poolKey, newObjectPool);
            }

        }
    }

    #region Pool Callbacks
    private void OnGetFromPool(Entity entity)
    {
        entity.OnSpawn();
    }

    private void OnReleaseToPool(Entity entity)
    {
        entity.OnDespawn();
    }

    private void OnDestroyPoolObject(Entity entity)
    {
        Destroy(entity);
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

    public void SpawnEnemy()
    {
        if (currentMonsterWeight >= MAX_MONSTER_WEIGHT)
        {
            return;
        }

        WeightedEnemy monster = PickMonster();
        Vector3 spawnPosition = PickSpawnLocation();

        Entity spawnedEnemy = currentEntityObjectPool[monster.prefab.name].Get();

        if (spawnedEnemy == null)
        {
            return;
        }

        spawnedEnemy.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        
        if (spawnedEnemy.TryGetComponent(out Enemy enemyComponent))
        {
            currentEnemyList.Add(enemyComponent);
        }

        currentMonsterWeight += monster.spawnWeight;
    }
}
