using System.Data;
using System;
using UnityEngine;

public class HitboxComponent : MonoBehaviour
{
    public event Action<HurtboxComponent> HitHurtbox;

    [SerializeField] private StatComponent statComponent;

    public float GetDamageAmount()
    {
        return statComponent.GetStat(StatType.ATTACK).GetCurrentValue();
    }

    public void RegisterHurtboxHit(HurtboxComponent hurtbox)
    {
        HitHurtbox?.Invoke(hurtbox);
    }
}

