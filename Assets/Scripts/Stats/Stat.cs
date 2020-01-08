using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float value;
    public float minValue;
    public float maxValue;
    public int upgradeLevels;
    public int currentUpgradeLevel;

    public Stat(float value, float minValue, float maxValue, int currentUpgradeLevel, int upgradeLevels)
    {
        this.value = value;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.upgradeLevels = upgradeLevels;
        this.currentUpgradeLevel = currentUpgradeLevel;
    }

    public bool Upgrade()
    {
        if(CanUpgrade())
        {
            value = GetNextLevelValue();
            currentUpgradeLevel++;
            return true;
        }

        return false;
    }

    public bool CanUpgrade()
    {
        if (currentUpgradeLevel < upgradeLevels)
        {
            return true;
        }
        return false;
    }

    public float GetNextLevelValue() // TODO handle max level
    {
        return minValue + (maxValue - minValue) / upgradeLevels * (currentUpgradeLevel + 1);
    }
}
