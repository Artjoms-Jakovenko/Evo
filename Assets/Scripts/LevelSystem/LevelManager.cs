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

    public static void RecordLevelCompletion(string levelName, int starCount)
    {
        SaveData saveData = SaveSystem.Load();
        if (!saveData.levelProgresses.ContainsKey(levelName)) // TODO also handle updating completed levels
        {
            saveData.levelProgresses.Add(levelName, new LevelProgress()); // TODO stars and stuff
            saveData.levelProgresses[levelName].starCount = starCount;
            saveData.levelProgresses.Add(GetNextLevelName(levelName), new LevelProgress()); // TODO handle max level // TODO handle if next level is already unlocked
            UnlockLevel(levelName); // TODO adjust with raii savesystem
        }
        else if (starCount > saveData.levelProgresses[levelName].starCount)
        {
            saveData.levelProgresses[levelName].starCount = starCount;
        }

        SaveSystem.Save(saveData);
    }

    public static void UnlockLevel(LevelEnum levelEnum)
    {
        UnlockLevel(GetLevelName(levelEnum));
    }

    private static void UnlockLevel(string levelName)
    {
        SaveData saveData = SaveSystem.Load();
        if (!saveData.levelProgresses.ContainsKey(levelName))
        {
            if (!levelNameMapping.ContainsKey(levelName))
            {
                Debug.LogError(levelName + ": such level does not exist.");
            }
            saveData.levelProgresses.Add(levelName, new LevelProgress());
        }
        saveData.levelProgresses[levelName].unlocked = true;
    }

    public static LevelEnum GetLevelEnum(string levelSceneName)
    {
        return levelNameMapping[levelSceneName];
    }

    public static string GetNextLevelName(string currentLevelName)
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
        SaveData saveData = SaveSystem.Load();
        return saveData.levelProgresses[GetLevelName(levelEnum)].unlocked;
    }

    public static void StartLevel(LevelEnum levelEnum)
    {
        SceneManager.LoadScene(GetLevelName(levelEnum));
    }
}
