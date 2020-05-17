using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorBlobStats : BlobStats
{
    private void Awake()
    {
        base.stats = SaveSystem.saveData.blobData[0];
    }
}
