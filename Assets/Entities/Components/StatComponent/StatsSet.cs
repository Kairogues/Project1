using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

[CreateAssetMenu(fileName = "StatsSet", menuName = "Scriptable Objects/StatsSet")]
public class StatsSet : ScriptableObject
{
    [SerializeField] private List<Stats> statsList = new List<Stats>();

    public List<Stats> GetStatsList()
    {
        return statsList;
    }

    public void AddStat(Stats newStat)
    {
        if (GetStatsInCurrentList(newStat.GetStatType()) != null)
        {
            UnityEngine.Debug.LogWarning("Duplicated Stat Type found!");
            return;
        }

        statsList.Add(newStat);
    }

    public bool RemoveStat(StatType type)
    {
        Stats target = GetStatsInCurrentList(type);
        
        if (target != null)
        {
            statsList.Remove(target);
            return true;
        }
        
        return false;
    }

    public Stats GetStatsInCurrentList(StatType type)
    {
        foreach (Stats stats in statsList)
        {
            if (stats.GetStatType() == type)
            {
                return stats;
            }
        }
        return null;
    }
}
