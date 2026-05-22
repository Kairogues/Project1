using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

[CreateAssetMenu(fileName = "StatsSet", menuName = "Scriptable Objects/StatsSet")]
public class StatsSet : ScriptableObject
{
    [SerializeField] private List<Stats> statsList = new List<Stats> {
        new Stats(StatType.HEALTH, 100.0f, true, 100.0f),
        new Stats(StatType.HEALTH_REGEN, 0.25f, false, 0.0f),
        new Stats(StatType.ATTACK, 100.0f, false, 0.0f), 
        new Stats(StatType.ATTACK_SPEED, 2.0f, false, 0.0f),
        new Stats(StatType.ARMOR, 0.0f, false, 0.0f),
        new Stats(StatType.MOVEMENT_SPEED, 100.0f, false, 0.0f)
    };

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
