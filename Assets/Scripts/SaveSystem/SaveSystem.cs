using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/evo";
    private static string SaveToString(SaveData saveData)
    {
        return JsonUtility.ToJson(saveData);
    }

    public static void Save(SaveData saveData) // TODO create save file on first game launch
    {
        using (var fileStream = new FileStream(savePath, FileMode.OpenOrCreate)) // TODO check if openorcreate is best
        {
            byte[] data = new UTF8Encoding(true).GetBytes(SaveToString(saveData));
            fileStream.Write(data, 0, data.Length);
        }
    }

    public static void MakeFirstSave()
    {
        SaveData saveData = new SaveData
        {
            money = 500, // TODO adjust numbers for release
            premiumMoney = 0
        };

        BlobStatsData blobStatsData = new BlobStatsData();
        saveData.blobData.Add(blobStatsData); // TODO dict key

        Save(saveData);
    }

    private static string LoadString() //  TODO throw error if file is not present
    {
        string fileContents = null;
        using (var fileStream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(fileStream))
            {
                fileContents = reader.ReadToEnd();
            }
        }
        return fileContents;
    }

    public static SaveData Load()
    {
        SaveData saveData = JsonUtility.FromJson<SaveData>(LoadString());
        return saveData;
    }
}
