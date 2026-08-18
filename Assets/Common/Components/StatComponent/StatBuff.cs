using UnityEngine;

public class StatBuff
{
    [SerializeField] private StatBuffType buffType;
    [SerializeField] private StatType statType;
    public StatType GetStatType()
    {
        return statType;
    }
    public StatBuffType GetBuffType()
    {
        return buffType;
    }

    [SerializeField] private float buffAmount;
    public float GetBuffAmount()
    {
        return buffAmount;
    }

    [SerializeField] private int buffID;
    public int GetBuffID()
    {
        return buffID;
    }
    public void SetBuffID(int ID)
    {
        buffID = ID;
    }
}
