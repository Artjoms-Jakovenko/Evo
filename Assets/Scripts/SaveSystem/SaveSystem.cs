using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class SaveSystem
{
    private readonly static string savePath = Application.persistentDataPath + "/evo.json";
    private readonly static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();

    private static SaveData _saveData; // TODO singleton pattern on get
    public static SaveData saveData
    {
        get
        {
            if(_saveData == null)
            {
                _saveData = Load();
            }
            return _saveData;
        }
        set
        {
            _saveData = value;
        }
    }

    static SaveSystem()
    {
        jsonSerializerSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;
    }

    private static string SaveToString(SaveData saveData)
    {
        return JsonConvert.SerializeObject(saveData, jsonSerializerSettings);
    }

    public static void Save()
    {
        using (var fileStream = new FileStream(savePath, FileMode.Create))
        {
            byte[] data = new UTF8Encoding(true).GetBytes(SaveToString(_saveData));
            fileStream.Write(data, 0, data.Length);
        }
    }

    private static void MakeFirstSave()
    {
        _saveData = new SaveData();

        _saveData.inventory.AddToInventory(InventoryEnum.Money, 10000);

        _saveData.lastSelectedBlobInUpgradeShop = 0; // TODO must match first blob id

        BlobStatsData blobStatsData = BlobInstantiator.CreateBlob(BlobType.Survivor);
        _saveData.blobData.Add(0, blobStatsData); // TODO dict key
        BlobStatsData blobStatsData2 = BlobInstantiator.CreateBlob(BlobType.Fighter);
        _saveData.blobData.Add(1, blobStatsData2); // TODO dict key

        Save();
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

    private static SaveData Load()
    {
        if (!File.Exists(savePath)) 
        {
            MakeFirstSave();
        }

        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(LoadString(), jsonSerializerSettings);

        return saveData;
    }
}
