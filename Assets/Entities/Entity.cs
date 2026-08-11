using UnityEngine;
using UnityEngine.Pool;

public abstract class Entity : MonoBehaviour
{
    #region EntityName
    [SerializeField] private string entityName;
    public string EntityName
    {
        set 
        { 
            entityName = value;
            name = entityName;
        } 
    }

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(entityName))
        {
            name = entityName;
        }
    }
    #endregion

    [SerializeField] protected IObjectPool<Entity> originPool;

    public void SetPool(IObjectPool<Entity> objectPool)
    {
        originPool = objectPool;
    }

    public virtual void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public virtual void ReleaseToPool()
    {
        if (originPool != null)
        {
            originPool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
