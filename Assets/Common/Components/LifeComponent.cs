using System;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    public event Action Died;

    [SerializeField] private StatsComponent statsComponent;
    private Stats healthStat;

    private void Start()
    {
        healthStat = statsComponent.GetStats(StatType.HEALTH);
        healthStat.MaximizeCurrentStat();

        UnityEngine.Debug.Log(gameObject.name + " has " + healthStat.GetCurrentValue() + " health");
    }

    public void Heal(float amount)
    {
        float newHealth = healthStat.GetCurrentValue() + amount;
        healthStat.UpdateStat(newHealth);

        UnityEngine.Debug.Log(gameObject.name + " healed " + amount);
        UnityEngine.Debug.Log(gameObject.name + " has " + healthStat.GetCurrentValue() + " health");
    }

    public void Damage(float amount)
    {
        float newHealth = healthStat.GetCurrentValue() - amount;
        
        if (newHealth <= 0)
        {
            healthStat.UpdateStat(0);
            Die();
        }

        healthStat.UpdateStat(newHealth);

        UnityEngine.Debug.Log(gameObject.name + " took " + amount + " amount");
        UnityEngine.Debug.Log(gameObject.name + " has " + healthStat.GetCurrentValue() + " health");
    }

    private void SubscribeToHealthChanged(Action<float, float, float> listener) 
    {
        statsComponent.SubscribeToStat(StatType.HEALTH, listener);
    }

    private void Die()
    {
        Died?.Invoke();

        print("You Died");
    }
}
