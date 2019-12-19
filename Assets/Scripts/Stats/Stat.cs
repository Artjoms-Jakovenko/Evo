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
    public int currentUpgradeLevel; // TODO override set to do the math
    public Stats statName; // TODO

    public Stat(float value, float minValue, float maxValue, int currentUpgradeLevel, int upgradeLevels, Stats statName)
    {
        this.value = value;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.upgradeLevels = upgradeLevels;
        this.currentUpgradeLevel = currentUpgradeLevel;
        this.statName = statName;
    }

    public bool Upgrade()
    {
        if(CanUpgrade())
        {
            currentUpgradeLevel++;
            value = (maxValue - minValue) / upgradeLevels * currentUpgradeLevel;
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
}
