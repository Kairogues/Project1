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
    
    // public void ApplyDebuff()
    // {
    //     
    // }
}
