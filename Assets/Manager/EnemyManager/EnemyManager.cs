using UnityEngine;
using System.Collections.Generic;

/// <summary>
///  EnemyManager is used to track every enemy in the current level, it controls the spawning/despawning and life cycle of every enemy
/// </summary>
public class EnemyManager : MonoBehaviour
{
    private const int MAX_MONSTER_WEIGHT = 300;
    private const float MINIMUM_SPAWN_DISTANCE = 15.0F;
    private const float MAXIMUM_SPAWN_DISTANCE = 25.0F;
    [SerializeField] private List<WeightedEnemy> currentEnemyPool;
    [SerializeField] private List<Enemy> currentEnemyList;
    private int currentMonsterWeight = 0;

    public void SetNewEnemyPool(List<WeightedEnemy> newEnemyPool)
    {
        currentEnemyPool = newEnemyPool;
        Debug.Log("SET NEW ENEMY POOL");
    }

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

        GameObject spawnedEnemy = Instantiate(monster.prefab, spawnPosition, Quaternion.identity);
        
        if (spawnedEnemy.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            currentEnemyList.Add(enemyComponent);
        }

        currentMonsterWeight += monster.spawnWeight;
    }
}
