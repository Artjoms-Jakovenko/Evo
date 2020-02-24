using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelInfoData
{
    public static List<BlobStatsData> GetLevelEnemies(LevelEnum levelEnum)
    {
        List<BlobStatsData> blobStatsDatas = new List<BlobStatsData>();
        switch (levelEnum)
        {
            case LevelEnum.TestingGround:
                blobStatsDatas.Add(BlobInstantiator.CreateBlob(BlobType.Fighter));
                break;
            case LevelEnum.IntroLevel1:
                // No enemies on this level
                break;
            case LevelEnum.IntroLevel2:
                blobStatsDatas.Add(BlobInstantiator.CreateBlob(BlobType.Survivor));
                break;
            case LevelEnum.IntroLevel3:
                blobStatsDatas.Add(BlobInstantiator.CreateBlob(BlobType.Fighter));
                break;
            default:
                Debug.LogError("LevelEnemyInfo was not found.");
                break;
        }
        return blobStatsDatas;
    }

    public static Dictionary<InventoryEnum, int> GetLevelRewards(LevelEnum levelEnum, int lastAchievedStars, int achievedStars)
    {
        LevelRewards levelRewards = new LevelRewards();

        switch (levelEnum)
        {
            case LevelEnum.TestingGround:
                levelRewards.participationReward.Add(InventoryEnum.Money, 3); // TODO rebalance
                // No rewards in test level
                break;
            case LevelEnum.IntroLevel1:
                levelRewards.participationReward.Add(InventoryEnum.Money, 3); // TODO rebalance
                levelRewards.victoryReward.Add(InventoryEnum.Money, 7); // TODO rebalance
                levelRewards.oneStarReward.Add(InventoryEnum.Money, 10); // TODO rebalance
                levelRewards.twoStarReward.Add(InventoryEnum.Money, 10); // TODO rebalance
                levelRewards.threeStarReward.Add(InventoryEnum.Money, 20); // TODO rebalance
                break;
            case LevelEnum.IntroLevel2:
                // TODO
                break;
            case LevelEnum.IntroLevel3:
                // TODO
                break;
            default:
                Debug.LogError("LevelRewardInfo was not found.");
                break;
        }

        Dictionary<InventoryEnum, int> rewards = levelRewards.GetLevelRewards(lastAchievedStars, achievedStars);

        return rewards;
    }

    public static List<ILevelGoal> GetLevelGoals(LevelEnum levelEnum)
    {
        List<ILevelGoal> levelGoals = new List<ILevelGoal>();

        switch (levelEnum)
        {
            case LevelEnum.TestingGround:
                levelGoals.Add(new SurviveGoal(10.0F));
                levelGoals.Add(new SurviveGoal(20.0F));
                levelGoals.Add(new SurviveGoal(90.0F));
                break;
            case LevelEnum.IntroLevel1:
                break;
            case LevelEnum.IntroLevel2:
                break;
            case LevelEnum.IntroLevel3:
                break;
            default:
                Debug.LogError("LevelGoalInfo was not found.");
                break;
        }

        if(levelGoals.Count != 3)
        {
            Debug.LogError("Level goal number is not equal to three:" + levelGoals.Count);
        }

        return levelGoals;
    }
}
