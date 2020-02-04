using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public int maxBlobCount;
    string nextLevelName;
    string participationReward;
    string oneStarReward;
    string twoStarReward;
    string threeStarReward;
    string completionReward;

    private void Awake()
    {
        GetLevelEnemies(LevelManager.GetLevelEnum(SceneManager.GetActiveScene().name));
    }

    public List<BlobStatsData> GetLevelEnemies(LevelEnum levelEnum)
    {
        List<BlobStatsData> blobStatsDatas = new List<BlobStatsData>();
        switch (levelEnum)
        {
            case LevelEnum.TestingGround:
                blobStatsDatas.Add(BlobInstantiator.CreateBlob(BlobType.Fighter));
                break;
            default:
                Debug.LogError("LevelEnemyInfo was not found.");
                break;
        }
        return blobStatsDatas; // TODO
    }
}
