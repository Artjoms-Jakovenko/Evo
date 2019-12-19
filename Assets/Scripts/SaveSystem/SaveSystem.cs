using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class SaveSystem
{
    private static string SaveToString()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMoney playerMoney = player.GetComponent<PlayerMoney>();

        SaveData saveData = new SaveData
        {
            money = playerMoney.coins,
            premiumMoney = playerMoney.premiumCoins
        };

        return JsonUtility.ToJson(saveData);
    }

    public static void Save()
    {
        string savePath = Application.persistentDataPath + "/evo";
        using (var fileStream = new FileStream(savePath, FileMode.OpenOrCreate))
        {
            byte[] data = new UTF8Encoding(true).GetBytes(SaveToString());
            fileStream.Write(data, 0, data.Length);
        }
    }

    private static string LoadString()
    {
        return null; // TODO
    }

    public static SaveData Load()
    {
        return null; // TODO
    }
}
