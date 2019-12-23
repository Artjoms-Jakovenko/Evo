using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveDataUtility
{
    public static void PayMoney(SaveData saveData, int amount)
    {
        saveData.money -= amount;
    }

    public static int GetMoney()
    {
        SaveData saveData = SaveSystem.Load();
        return saveData.money;
    }

    public static void SaveBlob() // TODO
    {

    }

    public static BlobStatsData GetBlobStats(SaveData saveData, int blobID)
    {
        return saveData.blobData[blobID];
    }
}
