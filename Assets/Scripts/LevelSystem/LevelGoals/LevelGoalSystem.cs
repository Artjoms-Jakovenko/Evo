using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoalSystem : MonoBehaviour
{
    Dictionary<ILevelGoal, bool> levelGoals = new Dictionary<ILevelGoal, bool>();
    private void Awake()
    {
        List<ILevelGoal> levelGoalsList = LevelInfoData.GetLevelGoals(LevelManager.GetLevelEnum(SceneManager.GetActiveScene().name));
        foreach(var levelGoal in levelGoalsList)
        {
            levelGoals.Add(levelGoal, false);
        }
    }

    private void Update() // TODO to be displayed in the game corner
    {
        /*foreach(var levelGoalKey in levelGoals.Keys)
        {
            levelGoals[levelGoalKey] = levelGoalKey.IsRequirementMet();
        }
        for (int i = 0; i < levelGoals.Count; i++)
        {
            
        }*/
    }

    public List<ILevelGoal> GetLevelGoals()
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
}
