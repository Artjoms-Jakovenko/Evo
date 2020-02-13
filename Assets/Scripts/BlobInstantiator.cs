using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BlobInstantiator
{
    public static GameObject GetBlobGameObject(BlobStatsData blobStats, TeamTag teamTag)
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
        AssignTeam(blob, teamTag);

        return blob;
    }

    private static void AssignTeam(GameObject blob, TeamTag teamName) // TODO make less nested loops
    {
        blob.GetComponent<TaggedObject>().teamTag = teamName;
        SkinnedMeshRenderer[] models = blob.GetComponentsInChildren<SkinnedMeshRenderer>(); // TODO make material with certain name modifyable
        for (int i = 0; i < models.Length; i++)
        {
            if(models[i].gameObject.name == "Cube")
            {
                List<Material> materials = new List<Material>();
                foreach(var material in models[i].materials)
                {
                    Material localMaterial = material;
                    if(material.name.Contains("Body"))
                    {
                        switch (teamName)
                        {
                            case TeamTag.Enemy:
                                localMaterial = Resources.Load("TeamMaterials/Enemy") as Material;
                                break;
                        }
                    }
                    materials.Add(localMaterial);
                }
                models[i].materials = materials.ToArray();
            }
        }
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

        blobStatsData.possibleActions.Add(ActionEnum.None);
        blobStatsData.possibleActions.Add(ActionEnum.Eat);

        blobStatsData.edibleTagCombinations.Add(new List<ObjectTag>() { ObjectTag.Edible, ObjectTag.Small, ObjectTag.Plant });

        switch (blobType)
        {
            case BlobType.Survivor:
                blobStatsData.possibleActions.Add(ActionEnum.RunAway);
                break;
            case BlobType.Fighter:
                blobStatsData.possibleActions.Add(ActionEnum.MeleeFight);
                blobStatsData.stats.Add(StatName.Strength, new Stat(1.0F, 1.0F, 2.0F, 0, 4));
                break;
        }

        return blobStatsData;
    }
}
