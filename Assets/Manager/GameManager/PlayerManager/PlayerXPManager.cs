using System;
using UnityEngine;

public class PlayerXPManager
{
    public event Action<int> GainedXP;
    public event Action<int> LeveledUp;
    private int currentXP = 0;
    private int currentMaxXP;
    private int currentLevel = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private XPGrowthConfig xpGrowthConfig;

    public PlayerXPManager()
    {
        currentMaxXP = GetXPRequiredForLevelUp(2);
    }
    
    public int GetXPRequiredForLevelUp(int level)
    {
        return xpGrowthConfig.GetXPForLevel(level);
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= currentMaxXP)
        {
            currentXP -= currentMaxXP;
            currentLevel++;

            LeveledUp?.Invoke(currentLevel);
            // await Task.Run();

            currentMaxXP = GetXPRequiredForLevelUp(currentLevel + 1);
        }

        GainedXP?.Invoke(currentXP);
    }
}
