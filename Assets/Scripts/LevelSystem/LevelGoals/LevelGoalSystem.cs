using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoalSystem : MonoBehaviour
{
    Dictionary<ILevelGoal, bool> levelGoals = new Dictionary<ILevelGoal, bool>();
    private void Awake()
    {
        LevelGoals _levelGoals = LevelInfoData.GetLevelGoals(LevelManager.GetLevelEnum(SceneManager.GetActiveScene().name));

        levelGoals.Add(_levelGoals.mainGoal, false);
        levelGoals.Add(_levelGoals.oneStarGoal, false);
        levelGoals.Add(_levelGoals.twoStarGoal, false);
        levelGoals.Add(_levelGoals.threeStarGoal, false);
    }

    public LevelGoals GetLevelGoals()
    {
        return LevelInfoData.GetLevelGoals(LevelManager.GetLevelEnum(SceneManager.GetActiveScene().name));
    }

    public int GetLevelCompletedGoalCount()
    {
        int completedGoalCount = 0;

        foreach (var levelGoal in levelGoals)
        {
            if (levelGoal.Key.IsRequirementMet())
            {
                completedGoalCount++;
            }
        }

        return completedGoalCount;
    }

    bool IsLevelCompleted() // TODO
    {
        return false;
    }
}
