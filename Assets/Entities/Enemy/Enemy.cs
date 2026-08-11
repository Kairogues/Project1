using UnityEngine;
using UnityEngine.Pool;

public class Enemy : Entity
{
    public override void OnSpawn()
    {
        base.OnSpawn();
        
        // Reset riêng cho Enemy
    }

    public void TakeDamage(float amount)
    {
        
    }

    protected virtual void Die()
    {
        // Khi chết thì tự chui về Pool
        ReleaseToPool();
    }
}
