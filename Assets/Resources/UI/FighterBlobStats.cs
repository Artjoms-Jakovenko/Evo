using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterBlobStats : BlobStats
{
    private void Awake()
    {
        base.stats = SaveSystem.saveData.blobData[1];
    }
}
