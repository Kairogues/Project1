using UnityEngine;

public enum BuffType
{
    ADD,
    MULTIPLY
}

public class Buff
{
    [SerializeField] private BuffType buffType;
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

    public BuffType GetBuffType()
    {
        return buffType;
    }

    public float GetBuffAmount()
    {
        return buffAmount;
    }
}
