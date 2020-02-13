using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRewardsSystem
{
    public static void AdministerRewards(LevelEnum levelName, int starsAchieved) // TODO
    {
        SaveData saveData = SaveSystem.Load();

        Dictionary<InventoryEnum, int> rewards = LevelInfoData.GetLevelRewards(levelName, 0, starsAchieved); // TODO read stars from savefile

        foreach (var reward in rewards)
        {
            saveData.inventory.AddToInventory(reward.Key, reward.Value);
        }

        SaveSystem.Save(saveData);
    }
}
