using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    private static List<LevelEnum> levelOrder = new List<LevelEnum>()
    {
        LevelEnum.TestingGround, // TODO remove
        LevelEnum.IntroLevel1,
        LevelEnum.IntroLevel2,
        LevelEnum.IntroLevel3,
    };
    
    private static Dictionary<string, LevelEnum> levelNameMapping = new Dictionary<string, LevelEnum>()
    {
        { "TestingGround", LevelEnum.TestingGround},
        { "IntroLevel1", LevelEnum.IntroLevel1},
        { "IntroLevel2", LevelEnum.IntroLevel2},
        { "IntroLevel3", LevelEnum.IntroLevel3},
    };

    public static void RecordLevelCompletion(string levelName, bool completed, int starCount)
    {
        SaveData saveData = SaveSystem.saveData;
        if (!saveData.levelProgresses.ContainsKey(levelName)) // TODO also handle updating completed levels
        {
            saveData.levelProgresses.Add(levelName, new LevelProgress()); // TODO stars and stuff
            saveData.levelProgresses[levelName].starCount = starCount;
        }
        else if (starCount > saveData.levelProgresses[levelName].starCount)
        {
            saveData.levelProgresses[levelName].starCount = starCount;
        }

        string nextLevelName = GetNextLevelName(levelName);
        if (nextLevelName != null && completed)
        {
            saveData.levelProgresses[levelName].completed = true;
            UnlockLevel(nextLevelName);
        }

        SaveSystem.Save();
    }

    public static void UnlockLevel(LevelEnum levelEnum)
    {
        UnlockLevel(GetLevelName(levelEnum));
    }

    private static void UnlockLevel(string levelName)
    {
        if (!SaveSystem.saveData.levelProgresses.ContainsKey(levelName))
        {
            if (!levelNameMapping.ContainsKey(levelName))
            {
                Debug.LogError(levelName + ": such level does not exist.");
            }
            SaveSystem.saveData.levelProgresses.Add(levelName, new LevelProgress());
        }
        SaveSystem.saveData.levelProgresses[levelName].unlocked = true;
    }

    public static LevelEnum GetLevelEnum(string levelSceneName)
    {
        return levelNameMapping[levelSceneName];
    }

    private static string GetNextLevelName(string currentLevelName)
    {
        int nextLevelNumber = levelOrder.FindIndex(x => x == levelNameMapping[currentLevelName]) + 1;
        if (nextLevelNumber >= levelOrder.Count)
        {
            return null;
        }
        return GetLevelName(levelOrder[nextLevelNumber]);
    }

    private static string GetLevelName(LevelEnum levelEnum)
    {
        return levelNameMapping.FirstOrDefault(x => x.Value == levelEnum).Key;
    }

    public static bool IsLevelUnlocked(LevelEnum levelEnum)
    {
        string levelName = GetLevelName(levelEnum);
        if (SaveSystem.saveData.levelProgresses.ContainsKey(levelName))
        {
            return SaveSystem.saveData.levelProgresses[levelName].unlocked;
        }
        else
        {
            return false;
        }
    }

    public static bool IsLevelCompleted(LevelEnum levelEnum)
    {
        string levelName = GetLevelName(levelEnum);
        if (SaveSystem.saveData.levelProgresses.ContainsKey(levelName))
        {
            return SaveSystem.saveData.levelProgresses[levelName].completed;
        }
        else
        {
            Debug.Log("Level info is missing from savefile.");
            return false;
        }
    }

    public static bool IsNextLevelUnlocked(LevelEnum levelEnum)
    {
        string levelName = GetNextLevelName(GetLevelName(levelEnum));
        if (SaveSystem.saveData.levelProgresses.ContainsKey(levelName))
        {
            return SaveSystem.saveData.levelProgresses[levelName].unlocked;
        }
        else
        {
            return false;
        }
    }

    public static void StartLevel(LevelEnum levelEnum)
    {
        SceneManager.LoadScene(GetLevelName(levelEnum));
    }

    public static void Load(LevelEnum levelEnum, bool loadNextLevel = false)
    {
        if (loadNextLevel)
        {
            SceneManager.LoadScene(GetNextLevelName(GetLevelName(levelEnum))); // Should not be called on the last level
        }
        else
        {
            SceneManager.LoadScene(GetLevelName(levelEnum));
        }
    }

    public static LevelProgress GetLevelProgress(LevelEnum levelEnum)
    {
        return SaveSystem.saveData.levelProgresses[GetLevelName(levelEnum)];
    }

    public static int GetLevelStarCount(LevelEnum levelEnum)
    {
        string levelName = GetLevelName(levelEnum);
        if (SaveSystem.saveData.levelProgresses.ContainsKey(levelName))
        {
            return SaveSystem.saveData.levelProgresses[levelName].starCount;
        }
        else
        {
            Debug.Log("Level info is missing from savefile.");
            return 0;
        }
    }
}
