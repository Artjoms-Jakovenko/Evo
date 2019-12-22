using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveDataUtility
{
    public static void PayMoney(int amount)
    {
        SaveData saveData = SaveSystem.Load();
        saveData.money -= amount;
        SaveSystem.Save(saveData);
    }

    public static int GetMoney()
    {
        SaveData saveData = SaveSystem.Load();
        return saveData.money;
    }

    public static void SaveBlob() // TODO
    {

    }

    public static BlobStatsData GetBlobStats(SaveData saveData, long blobID)
    {
        return saveData.blobData.Where(x => x.id == blobID).FirstOrDefault();
    }
}
