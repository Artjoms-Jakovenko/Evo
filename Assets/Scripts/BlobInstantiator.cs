using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlobInstantiator
{
    public static GameObject GetBlobGameObject(BlobStatsData blobStats) // TODO finish
    {
        GameObject gameObject = GameObject.Instantiate(Resources.Load("Blob") as GameObject);

        gameObject.GetComponent<BlobStats>().stats = blobStats;

        gameObject.GetComponent<Hunger>().energy = blobStats.stats[StatName.MaxEnergy].value / 2.0F;

        return gameObject;
    }
}
