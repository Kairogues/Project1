using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] protected PooledObject pooledObjectComponent;
    private int spawnWeight;

    public int GetSpawnWeight()
    {
        return spawnWeight;
    }

    public void SetSpawnWeight(int newSpawnWeight)
    {
        spawnWeight = newSpawnWeight;
    }

    protected virtual void Die()
    {
        pooledObjectComponent.ReleaseToPool();
    }

    // IPoolable
    public virtual void OnSpawn()
    {
        // Reset riêng cho Enemy
        GameManager.Instance.entityManager.RegisterEnemy(this);
    }

    public virtual void OnDespawn()
    {
        GameManager.Instance.entityManager.UnregisterEnemy(this);
    }
}
