﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public string version = "1.0";
    public string protection = "FFFF";
    public int money;
    public int premiumMoney;
    public List<BlobStatsData> blobData = new List<BlobStatsData>();
}
