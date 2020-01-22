using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlobInstantiator
{
    public static GameObject GetBlobGameObject(BlobStatsData blobStats) // TODO finish
    {
        GameObject gameObject = GameObject.Instantiate(Resources.Load("Blob") as GameObject);

        gameObject.GetComponent<BlobStats>().stats = blobStats;

        return gameObject;
    }
}
