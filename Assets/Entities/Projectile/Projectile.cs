using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] protected PooledObject pooledObjectComponent;



    private void Update()
    {
        // transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        
        // timer -= Time.deltaTime;
        // if (timer <= 0f)
        // {
        //    ReleaseToPool(); // Tự chết khi hết time
        // }
    }


    private void OnTriggerEnter(Collider other)
    {
        ReleaseToPool(); // Tự chết khi chạm mục tiêu
    }


    // IPoolable
    public virtual void OnSpawn()
    {
        GameManager.Instance.entityManager.RegisterProjectile(this);
    }


    public virtual void OnDespawn()
    {
        GameManager.Instance.entityManager.UnregisterProjectile(this);
    }


    protected virtual void ReleaseToPool()
    {
        pooledObjectComponent.ReleaseToPool();
    }
}
