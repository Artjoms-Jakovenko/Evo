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

    private void GetLevelEnemies(LevelEnum levelEnum)
    {
        switch (levelEnum)
        {
            // TODO
        }
    }
}
