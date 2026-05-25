using System;
using UnityEngine;

public class HurtboxComponent : MonoBehaviour
{
    public event Action<float> TookDamage;

    [SerializeField] private EntityType entityType = EntityType.ENEMY;
    [SerializeField] private LifeComponent lifeComponent;

    public EntityType GetEntityType()
    {
        return entityType;
    }

    public void TakeDamge(float damage)
    {
        lifeComponent.Damage(damage);

        TookDamage?.Invoke(damage);
    }
    
    // public void ApplyDebuff()
    // {
    //     
    // }
}
