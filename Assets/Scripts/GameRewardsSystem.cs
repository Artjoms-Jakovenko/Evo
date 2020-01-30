using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRewardsSystem
{
    public static void AdministerRewards() // TODO
    {
        SaveData saveData = SaveSystem.Load();
        saveData.money += 500;
        SaveSystem.Save(saveData);
    }
    // TODO rewards based on stars + dictionary of levels
}
