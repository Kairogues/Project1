using System;
using UnityEngine;
using System.Collections.Generic;
using System.Data;

public class StatsComponent : MonoBehaviour
{
    [SerializeField] private StatsSet statsSet;

    [SerializeField] private Dictionary<int, Buff> buffDictionary = new Dictionary<int, Buff>();
    private int nextBuffID = 0;
    
    public void RecalculateStatAfterBuff(StatType type)
    {
        Stats stat = statsSet.GetStatsInCurrentList(type);
        if (stat == null)
        {
            UnityEngine.Debug.LogWarning("Trying to recalculate non-existing stat!");
            return;
        }

        float addAmount = 0.0f;
        float multiplyAmount = 0.0f;

        foreach (Buff buff in buffDictionary.Values)
        {
            if (buff.GetStatType() == type)
            {
                if (buff.GetBuffType() == BuffType.ADD)
                {
                    addAmount += buff.GetBuffAmount();
                } else if (buff.GetBuffType() == BuffType.MULTIPLY)
                {
                    multiplyAmount += buff.GetBuffAmount();
                }
            }
        }

        float currentStatValue = statsSet.GetStatsInCurrentList(type).GetCurrentValue();
        currentStatValue += addAmount;
        currentStatValue *= 1.0f + multiplyAmount;
        statsSet.GetStatsInCurrentList(type).UpdateStat(currentStatValue);
    }

    // Temporary, work just fine but I really do not like this
    public int AddBuff(Buff newBuff)
    {
        int currentBuffID = nextBuffID;
        newBuff.SetBuffID(currentBuffID);
        
        buffDictionary.Add(currentBuffID, newBuff);
        
        nextBuffID++;
        return currentBuffID;
    }

    // Temporary, work just fine but I really do not like this
    public bool RemoveBuff(int buffIDToRemove)
    {
        if (buffDictionary.ContainsKey(buffIDToRemove))
        {
            buffDictionary.Remove(buffIDToRemove);
            return true;
        }

        return false;
    }

    public Stats GetStats(StatType type)
    {
        Stats stat = statsSet.GetStatsInCurrentList(type);
        if (stat == null)
        {
            UnityEngine.Debug.LogWarning("Trying to access non-existing stat!");
            return null;
        }

        return stat;
    }

    public void SubscribeToStat(StatType type, Action<float, float, float> listener)
    {
        Stats stat = GetStats(type);
        stat.OnStatChanged += listener;
    }

    public void UnSubscribeToStat(StatType type, Action<float, float, float> listener)
    {
        Stats stat = GetStats(type);
        stat.OnStatChanged -= listener;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
