using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BlobInstantiator
{
    public static GameObject GetBlobGameObject(BlobStatsData blobStats) // TODO finish
    {
        GameObject blob = null;
        switch (blobStats.blobType)
        {
            case BlobType.Survivor:
                blob = GameObject.Instantiate(Resources.Load("Blob") as GameObject);
                break;
            case BlobType.Fighter:
                blob = GameObject.Instantiate(Resources.Load("Fighter") as GameObject);
                break;
        }

        blob.GetComponent<BlobStats>().stats = blobStats;

        return blob;
    }

    public static BlobStatsData CreateBlob(BlobType blobType)
    {
        BlobStatsData blobStatsData = new BlobStatsData();

        blobStatsData.blobType = blobType;

        blobStatsData.stats.Add(StatName.Speed, new Stat(1.0F, 1.0F, 2.0F, 0, 4));
        blobStatsData.stats.Add(StatName.Health, new Stat(1.0F, 1.0F, 2.0F, 0, 4));
        blobStatsData.stats.Add(StatName.MaxEnergy, new Stat(20.0F, 20.0F, 40.0F, 0, 4));
        blobStatsData.stats.Add(StatName.Sight, new Stat(5.0F, 5.0F, 10.0F, 0, 4));
        blobStatsData.stats.Add(StatName.ReactionTime, new Stat(3.0F, 3.0F, 2.0F, 0, 4));
        //{ StatName.Strength, new Stat(1.0F, 1.0F, 2.0F, 0, 4) },
        //public Stat<float> StartingEnergy = new Stat<float>(10.0F, 10.0F, 50.0F, 4);

        blobStatsData.possibleActions.Add(Action.None);
        blobStatsData.possibleActions.Add(Action.Eat);

        blobStatsData.edibleTagCombinations.Add(new List<ObjectTag>() { ObjectTag.Edible, ObjectTag.Small, ObjectTag.Plant });

        switch (blobType)
        {
            case BlobType.Survivor:

                break;
            case BlobType.Fighter:
                blobStatsData.possibleActions.Add(Action.MeleeFight);
                break;
        }

        return blobStatsData; // TODO
    }
}
