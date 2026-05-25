using System;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public event Action<float, float, float> StatChanged;

    [SerializeField] private StatType statType;
    [SerializeField] private float currentValue;
    [SerializeField] private bool useMaxValue;
    [SerializeField] private float maxValue;

    public Stats(StatType type = StatType.HEALTH, float initCurrentValue = 0, bool initUseMaxValue = false, float initMaxValue = 0)
    {
        statType = type;
        currentValue = initCurrentValue;
        useMaxValue = initUseMaxValue;
        maxValue = initMaxValue;
    }

    public void MaximizeCurrentStat()
    {
        currentValue = maxValue;
    }

    public StatType GetStatType()
    {
        return statType;
    }

    public float GetCurrentValue()
    {
        return currentValue;
    }

    public float GetMaxValue()
    {
        return maxValue;
    }

    public void UpdateStat(float newValue) 
    {
        float oldValue = currentValue;
        currentValue = newValue;
        if (useMaxValue && (currentValue > maxValue))
        {
            currentValue = maxValue;
        }

        StatChanged?.Invoke(oldValue, currentValue, maxValue);
    }

    public void UpdateMaxStat(float newMaxValue) 
    {
        maxValue = newMaxValue;
    }
}
