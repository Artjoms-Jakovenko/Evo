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
}
