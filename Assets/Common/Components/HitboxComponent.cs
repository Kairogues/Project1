using System.Data;
using System;
using UnityEngine;

public class HitboxComponent : MonoBehaviour
{
    public event Action<HurtboxComponent> HitHurtbox;

    [SerializeField] private StatsComponent statsComponent;

    public float GetDamageAmount()
    {
        return statsComponent.GetStats(StatType.ATTACK).GetCurrentValue();
    }

    public void RegisterHurtboxHit(HurtboxComponent hurtbox)
    {
        HitHurtbox?.Invoke(hurtbox);
    }
}

