using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRewards
{
    public Dictionary<InventoryEnum, int> participationReward = new Dictionary<InventoryEnum, int>();
    public Dictionary<InventoryEnum, int> victoryReward = new Dictionary<InventoryEnum, int>();
    public Dictionary<InventoryEnum, int> oneStarReward = new Dictionary<InventoryEnum, int>();
    public Dictionary<InventoryEnum, int> twoStarReward = new Dictionary<InventoryEnum, int>();
    public Dictionary<InventoryEnum, int> threeStarReward = new Dictionary<InventoryEnum, int>();

    public Dictionary<InventoryEnum, int> GetLevelRewards(int maxStarsAchieved, int starsAchieved)
    {
        Dictionary<InventoryEnum, int> rewards = new Dictionary<InventoryEnum, int>();

        AddCombineReward(rewards, participationReward);

        if(starsAchieved > 0)
        {
            AddCombineReward(rewards, victoryReward);
        }

        if(starsAchieved == 3 && maxStarsAchieved < 3)
        {
            AddCombineReward(rewards, threeStarReward);
        }

        if (starsAchieved >= 2 && maxStarsAchieved < 2)
        {
            AddCombineReward(rewards, twoStarReward);
        }

        if (starsAchieved >= 1 && maxStarsAchieved < 1)
        {
            AddCombineReward(rewards, oneStarReward);
        }

        return rewards;
    }

    private void AddCombineReward(Dictionary<InventoryEnum, int> dictionary, Dictionary<InventoryEnum, int> rewards)
    {
        foreach (var reward in rewards)
        {
            if (dictionary.ContainsKey(reward.Key))
            {
                dictionary[reward.Key] += reward.Value;
            }
            else
            {
                dictionary.Add(reward.Key, reward.Value);
            }
        }
    }
}
