using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlobInstantiator
{
    public static GameObject GetBlobGameObject(BlobStats blobStats)
    {
        GameObject gameObject = Resources.Load("Blob") as GameObject;
        gameObject.GetComponent<Hunger>().energy = blobStats.stats.stats[StatName.MaxEnergy].value / 2.0F;

        return gameObject;
    }
}
