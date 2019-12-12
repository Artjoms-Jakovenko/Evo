using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat<T>
{
    public T value;
    public T minValue;
    public T maxValue;
    public int upgradeLevels;
    public int currentUpgradeLevel { set; get; } // TODO override set to do the math

    public Stat(T value, T minValue, T maxValue, int currentUpgradeLevel, int upgradeLevels)
    {
        this.value = value;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.upgradeLevels = upgradeLevels;
    }
}
