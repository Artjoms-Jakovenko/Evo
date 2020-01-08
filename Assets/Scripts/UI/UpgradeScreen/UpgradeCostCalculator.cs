using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeSystem
{
    private static int CalculateUpgradeCost(Stat stat, float baselineValue, float baselineCost, float upgradeAmountCost)
    {
        int nextUpgradeLevel = stat.currentUpgradeLevel + 1;
        float cost = (stat.minValue / baselineValue * baselineCost - baselineCost + (stat.maxValue - stat.minValue) / stat.upgradeLevels * upgradeAmountCost) * nextUpgradeLevel * nextUpgradeLevel;
        return (int) cost;
    }

    // TODO make it an enum stat calculator
    public static int GetUpgradeCost(StatName statName, Stat stat)
    {
        switch (statName)
        {
            case StatName.Speed:
                return CalculateUpgradeCost(stat, 1.0F, 100.0F, 1000.0F);
            default:
                throw new System.Exception();
        }
        
    }
}
