using UnityEngine;

[System.Serializable]
public struct LevelTier
{
    public int upperThreshold;
    public int xpIncreasePerLevel;
}

[CreateAssetMenu(fileName = "XPGrowthConfig", menuName = "Scriptable Objects/XPGrowthConfig")]
public class XPGrowthConfig : ScriptableObject
{
    [SerializeField] public int baseXP = 5; // Level 1 -> 2
    [SerializeField] public LevelTier[] tiers;



    public int GetXPForLevel(int level)
    {
        if (level <= 1) return baseXP;

        int currentXPRequirement = baseXP;
        int previousLevelMark = 2;

        if (tiers != null)
        {
            foreach (var tier in tiers)
            {
                if (level <= tier.upperThreshold)
                {
                    currentXPRequirement += tier.xpIncreasePerLevel * (level - previousLevelMark);
                    return currentXPRequirement;
                }
                else
                {
                    currentXPRequirement += tier.xpIncreasePerLevel * (tier.upperThreshold - previousLevelMark + 1);
                    previousLevelMark = tier.upperThreshold + 1;
                }
            }

            if (tiers.Length > 0)
            {
                var lastTier = tiers[tiers.Length - 1];
                currentXPRequirement += lastTier.xpIncreasePerLevel * (level - previousLevelMark);
            }
        }

        return currentXPRequirement;
    }
}