using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

/// <summary>
///  EntityManager is used to track every entity in the current level, it controls the spawning/despawning and life cycle of every enemy
/// </summary>
public class EntityManager : MonoBehaviour
{
    private const int MAX_MONSTER_WEIGHT = 300;
    private const float MINIMUM_SPAWN_DISTANCE = 15.0F;
    private const float MAXIMUM_SPAWN_DISTANCE = 25.0F;
    private const float XP_MERGE_RANGE = 200.0F;
    private const float XP_DESPAWN_RANGE = 300.0F;
    private const float ENEMY_DESPAWN_RANGE = 300.0F;

    // =============== ENEMY ===============
    [SerializeField] private List<EnemySpawnEntry> currentEnemyPool;
    [SerializeField] private List<Enemy> currentEnemyList = new();
    private int currentMonsterWeight = 0;
    public void RegisterEnemy(Enemy enemy) => currentEnemyList.Add(enemy);
    public void UnregisterEnemy(Enemy enemy) => currentEnemyList.Remove(enemy);
    // =====================================

    // ============ PROJECTILE =============
    [SerializeField] private List<Projectile> currentProjectileList = new();
    public void RegisterProjectile(Projectile projectile) => currentProjectileList.Add(projectile);
    public void UnregisterProjectile(Projectile projectile) => currentProjectileList.Remove(projectile);
    // =====================================

    // ============== XP ORB ===============
    [SerializeField] private List<Pickupable> currentXPOrbList = new();
    public void RegisterXPOrb(Pickupable xpOrb) => currentXPOrbList.Add(xpOrb);
    public void UnregisterXPOrb(Pickupable xpOrb) => currentXPOrbList.Remove(xpOrb);
    // =====================================

    // ============ PICKUPABLE =============
    [SerializeField] private List<Pickupable> currentPickupableList = new();
    public void RegisterPickupable(Pickupable pickupable) => currentPickupableList.Add(pickupable);
    public void UnregisterPickupable(Pickupable pickupable) => currentPickupableList.Remove(pickupable);
    // =====================================

    private void Awake()
    {
        // enemySpawner.SetEntityManager(this);
        // projectileSpawner.SetEntityManager(this);
        // xpOrbSpawner.SetEntityManager(this);
        // pickupableSpawner.SetEntityManager(this);
    }


    public List<EnemySpawnEntry> GetNewEnemyPool()
    {
        return currentEnemyPool;
    }

    public void SetNewEnemyPool(List<EnemySpawnEntry> newEnemyPool)
    {
        currentEnemyPool = newEnemyPool;
    }

    private EnemySpawnEntry PickMonster()
    {
        int random = Random.Range(0, 100);
        float accumulation = 0;
        
        foreach (EnemySpawnEntry enemy in currentEnemyPool)
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

        EnemySpawnEntry monster = PickMonster();
        Vector3 spawnPosition = PickSpawnLocation();

        GameObject spawnedEnemy = GameManager.Instance.poolManager.Spawn(monster.prefab.gameObject, spawnPosition, Quaternion.identity, this.transform);

        if (spawnedEnemy == null)
        {
            return;
        }

        currentMonsterWeight += monster.prefab.GetSpawnWeight();
    }

    public void DespawnEnemy(Enemy entity, int weight)
    {
        currentMonsterWeight = Mathf.Max(0, currentMonsterWeight - weight);
    }
}
