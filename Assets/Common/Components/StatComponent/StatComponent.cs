using System;
using UnityEngine;
using System.Collections.Generic;
using System.Data;

public class StatComponent : MonoBehaviour
{
    [SerializeField] private StatSet statSet;

    [SerializeField] private Dictionary<int, StatBuff> buffDictionary = new Dictionary<int, StatBuff>();
    private int nextBuffID = 0;
    
    public void RecalculateStatAfterBuff(StatType type)
    {
        Stat stat = statSet.GetStatInCurrentList(type);
        if (stat == null)
        {
            UnityEngine.Debug.LogWarning("Trying to recalculate non-existing stat!");
            return;
        }

        float addAmount = 0.0f;
        float multiplyAmount = 0.0f;

        foreach (StatBuff buff in buffDictionary.Values)
        {
            if (buff.GetStatType() == type)
            {
                if (buff.GetBuffType() == StatBuffType.ADD)
                {
                    addAmount += buff.GetBuffAmount();
                } else if (buff.GetBuffType() == StatBuffType.MULTIPLY)
                {
                    multiplyAmount += buff.GetBuffAmount();
                }
            }
        }

        float currentStatValue = statSet.GetStatInCurrentList(type).GetCurrentValue();
        currentStatValue += addAmount;
        currentStatValue *= 1.0f + multiplyAmount;
        statSet.GetStatInCurrentList(type).UpdateStat(currentStatValue);
    }

    // Temporary, work just fine but I really do not like this
    public int AddBuff(StatBuff newBuff)
    {
        int currentBuffID = nextBuffID;
        newBuff.SetBuffID(currentBuffID);
        
        buffDictionary.Add(currentBuffID, newBuff);

        RecalculateStatAfterBuff(newBuff.GetStatType());
        
        nextBuffID++;

        return currentBuffID;
    }

    // Temporary, work just fine but I really do not like this
    public bool RemoveBuff(int buffIDToRemove)
    {
        if (buffDictionary.ContainsKey(buffIDToRemove))
        {
            StatBuffType buffType = buffDictionary[buffIDToRemove];
            buffDictionary.Remove(buffIDToRemove);
            RecalculateStatAfterBuff(buffType);
            return true;
        }

        return false;
    }

    public Stat GetStat(StatType type)
    {
        Stat stat = statSet.GetStatInCurrentList(type);
        if (stat == null)
        {
            UnityEngine.Debug.LogWarning("Trying to access non-existing stat!");
            return null;
        }

        return stat;
    }

    public void SubscribeToStat(StatType type, Action<float, float, float> listener)
    {
        Stat stat = GetStat(type);
        stat.StatChanged += listener;
    }

    public void UnSubscribeToStat(StatType type, Action<float, float, float> listener)
    {
        Stat stat = GetStat(type);
        stat.StatChanged -= listener;
    }
}
