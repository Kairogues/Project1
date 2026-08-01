using UnityEngine;

public enum StatBuffType
{
    ADD,
    MULTIPLY
}

public class StatBuff
{
    [SerializeField] private StatBuffType buffType;
    [SerializeField] private StatType statType;
    [SerializeField] private float buffAmount;
    [SerializeField] private int buffID;

    public int GetBuffID()
    {
        return buffID;
    }

    public void SetBuffID(int ID)
    {
        buffID = ID;
    }

    public StatType GetStatType()
    {
        return statType;
    }

    public StatBuffType GetBuffType()
    {
        return buffType;
    }

    public float GetBuffAmount()
    {
        return buffAmount;
    }
}
