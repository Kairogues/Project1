using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    #region EnemyName
    [SerializeField] private string enemyName;
    public string EnemyName
    {
        set 
        { 
            enemyName = value; 
            name = enemyName;
        } 
    }

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(enemyName))
        {
            name = enemyName;
        }
    }
    #endregion

    [SerializeField] private IObjectPool<GameObject> objectPool;

    public void SetPool(IObjectPool<GameObject> objectPool)
    {
        this.objectPool = objectPool;
    }

    public void ReleaseToPool()
    {
        objectPool.Release(gameObject);
    }
}
