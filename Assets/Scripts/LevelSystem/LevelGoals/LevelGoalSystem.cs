using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoalSystem : MonoBehaviour
{
    LevelGoals levelGoals;
    private void Awake()
    {
        levelGoals = LevelInfoData.GetLevelGoals(LevelManager.GetLevelEnum(SceneManager.GetActiveScene().name));
    }

    public LevelGoals GetLevelGoals()
    {
        return LevelInfoData.GetLevelGoals(LevelManager.GetLevelEnum(SceneManager.GetActiveScene().name));
    }

    public int GetLevelCompletedGoalCount()
    {
        int completedGoalCount = 0;

        if (levelGoals.oneStarGoal.IsRequirementMet())
        {
            completedGoalCount++;
        }
        if (levelGoals.twoStarGoal.IsRequirementMet())
        {
            completedGoalCount++;
        }
        if (levelGoals.threeStarGoal.IsRequirementMet())
        {
            completedGoalCount++;
        }

        return completedGoalCount;
    }

    public bool IsLevelCompleted()
    {
        return levelGoals.mainGoal.IsRequirementMet();
    }
}
