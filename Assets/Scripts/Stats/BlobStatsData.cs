﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlobStatsData
{
    public BlobType blobType;
    public string blobName;

    public Dictionary<StatName, Stat> stats = new Dictionary<StatName, Stat>();
    public List<ActionEnum> possibleActions = new List<ActionEnum>();
    public List<List<ObjectTag>> edibleTagCombinations = new List<List<ObjectTag>>();

    public int maxXPLevel = 3;
    public int currentXPLevel = 0;
    public float blobXp = 0;
}
