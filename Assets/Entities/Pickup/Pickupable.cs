using UnityEngine;

public class Pickupable : MonoBehaviour, IPoolable
{
    [SerializeField] protected PooledObject pooledObjectComponent;



    private void OnTriggerEnter(Collider other)
    {
        
    }


    public virtual void OnSpawn()
    {
        GameManager.Instance.entityManager.RegisterPickupable(this);
    }


    public virtual void OnDespawn()
    {
        GameManager.Instance.entityManager.UnregisterPickupable(this);
    }


    protected virtual void ReleaseToPool()
    {
        pooledObjectComponent.ReleaseToPool();
    }


    protected virtual void OnDrop()
    {
        
    }


    protected virtual void OnPickup()
    {
        ReleaseToPool();
    }


    protected virtual void ProcessPickup()
    {
        
    }
}
