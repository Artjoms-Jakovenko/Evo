using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManager
{
    private static List<string> levels = new List<string>()
    {
        "TestingGround", // TODO remove
        "IntroLevel1",
        "IntroLevel2",
        "IntroLevel3",
    };

    public static void RecordLevelCompletion(string levelName)
    {
        SaveData saveData = SaveSystem.Load();
        if (!saveData.levelProgresses.ContainsKey(levelName)) // TODO also handle updating completed levels
        {
            saveData.levelProgresses.Add(levelName, new LevelProgress()); // TODO stars and stuff
            saveData.levelProgresses.Add(GetNextLevelName(levelName), new LevelProgress());
        }

        SaveSystem.Save(saveData);
    }

    public static string GetNextLevelName(string currentLevelName)
    {
        int nextLevelNumber = levels.FindIndex(x => x == currentLevelName) + 1;
        if (nextLevelNumber >= levels.Count)
        {
            return null;
        }
        return levels[nextLevelNumber];
    }
}
