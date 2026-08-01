using System;
using UnityEngine;

public class HurtboxComponent : MonoBehaviour
{
    public event Action<float> TookDamage;

    [SerializeField] private LifeComponent lifeComponent;

    public void TakeDamge(float damage)
    {
        lifeComponent.Damage(damage);

        TookDamage?.Invoke(damage);
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        HitboxComponent hitbox = hitInfo.GetComponent<HitboxComponent>();

        if (hitbox != null)
        {
            hitbox.RegisterHurtboxHit(this);
            TakeDamge(hitbox.GetDamageAmount());
        }
    }
    */
}
