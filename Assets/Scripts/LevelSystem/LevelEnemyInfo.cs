using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelEnemyInfo
{
    static List<BlobStatsData> GetLevelEnemies(string levelName)
    {
        List<BlobStatsData> blobStatsDatas = new List<BlobStatsData>();

        switch (levelName)
        {
            case "TestingGround":
                break;
            default:
                Debug.LogError("LevelEnemyInfo was not found for: " + levelName);
                break;
        }

        return blobStatsDatas;
    }
}
