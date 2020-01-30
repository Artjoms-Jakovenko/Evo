using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public string version = "1.0";
    public string protection = "FFFF";
    public int money;
    public int premiumMoney;
    public Dictionary<int, BlobStatsData> blobData = new Dictionary<int, BlobStatsData>();

    public void AddMoney(int money)
    {
        this.money += money;
    }

    public void PayMoney(int money)
    {
        this.money -= money; // TODO add checks if below 0
        if(money < 0)
        {
            Debug.LogError("Money is less than zero: " + money);
        }
    }
}
