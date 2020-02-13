using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public int maxBlobCount;

    public List<BlobStatsData> GetLevelEnemies(LevelEnum levelEnum)
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

    public Dictionary<InventoryEnum, int> GetLevelRewards(LevelEnum levelEnum, int lastAchievedStars, int achievedStars)
    {
        Dictionary<InventoryEnum, int> rewards = new Dictionary<InventoryEnum, int>();

        switch (levelEnum)
        {
            case LevelEnum.TestingGround:
                // No rewards in test level
                break;
            case LevelEnum.IntroLevel1:
                rewards.Add(InventoryEnum.Money, 20); // TODO rebalance
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

        return null; // TODO
    }
}
