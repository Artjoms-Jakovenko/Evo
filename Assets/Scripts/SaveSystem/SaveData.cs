using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public string version = "1.0";
    public string protection = "FFFF";
    public Inventory inventory = new Inventory();
    public Dictionary<int, BlobStatsData> blobData = new Dictionary<int, BlobStatsData>();
    public Dictionary<string, LevelProgress> levelProgresses = new Dictionary<string, LevelProgress>();

    public int lastSelectedBlobInUpgradeShop;
}
