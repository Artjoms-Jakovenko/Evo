using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            saveData.levelProgresses.Add(GetNextLevelName(levelName), new LevelProgress()); // TODO handle max level
        }
        else if (starCount > saveData.levelProgresses[levelName].starCount)
        {
            saveData.levelProgresses[levelName].starCount = starCount;
        }

        SaveSystem.Save(saveData);
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
        return levelNameMapping.FirstOrDefault(x => x.Value == levelOrder[nextLevelNumber]).Key;
    }
}
