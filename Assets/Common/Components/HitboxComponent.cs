using System.Data;
using UnityEngine;

public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private EntityType entityType = EntityType.ENEMY;
    [SerializeField] private StatsComponent statsComponent;
    private float damageAmount = 0.0f;

    private void Awake()
    {
        SetDamageAmount(statsComponent.GetStats(StatType.ATTACK).GetCurrentValue());
    }

    public EntityType GetEntityType()
    {
        return entityType;
    }

    public void SetEntityType(EntityType newEntityType)
    {
        entityType = newEntityType;
    }

    public void SetDamageAmount(float amount)
    {
        damageAmount = amount;
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        HurtboxComponent hurtbox = hitInfo.GetComponent<HurtboxComponent>();

        if (hurtbox != null)
        {  
            if ((entityType == EntityType.PLAYER && hurtbox.GetEntityType() == EntityType.ENEMY) ||
            (entityType == EntityType.ENEMY && hurtbox.GetEntityType() == EntityType.PLAYER))
            {
                hurtbox.TakeDamge(damageAmount);
            }
        }

        UnityEngine.Debug.Log(gameObject.name + " just hit " + hitInfo.name);

        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
