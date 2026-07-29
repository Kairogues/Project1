using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

[CreateAssetMenu(fileName = "StatSet", menuName = "Scriptable Objects/StatSet")]
public class StatSet : ScriptableObject
{
    [SerializeField] private List<Stat> statList = new List<Stat> {
        new Stat(StatType.HEALTH, 100.0f, true, 100.0f),
        new Stat(StatType.HEALTH_REGEN, 0.25f, false, 0.0f),
        new Stat(StatType.ATTACK, 100.0f, false, 0.0f), 
        new Stat(StatType.ATTACK_SPEED, 2.0f, false, 0.0f),
        new Stat(StatType.ARMOR, 0.0f, false, 0.0f),
        new Stat(StatType.MOVEMENT_SPEED, 100.0f, false, 0.0f)
    };

    public List<Stat> GetStatList()
    {
        return statList;
    }

    public void AddStat(Stat newStat)
    {
        if (GetStatInCurrentList(newStat.GetStatType()) != null)
        {
            UnityEngine.Debug.LogWarning("Duplicated Stat Type found!");
            return;
        }

        statList.Add(newStat);
    }

    public bool RemoveStat(StatType type)
    {
        Stat target = GetStatInCurrentList(type);
        
        if (target != null)
        {
            statList.Remove(target);
            return true;
        }
        
        return false;
    }

    public Stat GetStatInCurrentList(StatType type)
    {
        foreach (Stat stat in statList)
        {
            if (stat.GetStatType() == type)
            {
                return stat;
            }
        }
        return null;
    }
}
