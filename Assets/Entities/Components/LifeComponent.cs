using System;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    public event Action OnDied;

    [SerializeField] private StatsComponent statsComponent;
    private Stats healthStat;

    void Awake()
    {
        healthStat = statsComponent.GetStats(StatType.HEALTH);
        healthStat.MaximizeCurrentStat();
    }

    public void Heal(float amount)
    {
        float newHealth = healthStat.GetCurrentValue() + amount;
        healthStat.UpdateStat(newHealth);
    }

    public void Damage(float amount)
    {
        float newHealth = healthStat.GetCurrentValue() - amount;
        
        if (newHealth <= 0)
        {
            Die();
        }

        healthStat.UpdateStat(0);
    }

    private void SubscribeToHealthChanged(Action<float, float, float> listener) 
    {
        statsComponent.SubscribeToStat(StatType.HEALTH, listener);
    }

    private void Die()
    {
        OnDied?.Invoke();
        print("You Died");
    }
}
