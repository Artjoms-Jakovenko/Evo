using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayAction : IAction
{
    private readonly BlobMovement blobMovement;
    private readonly TeamTag teamTag;

    public RunAwayAction(GameObject blob)
    {
        blobMovement = blob.GetComponent<BlobMovement>();
        teamTag = blob.GetComponent<TaggedObject>().teamTag;
    }

    public float GetActionPriorityScore() // TODO run only from fighters
    {
        /*enemies = ObjectManager.GetInstance().GetAllEnemies(teamTag);

        float maxDistance = blobStats.stats.stats[StatName.Sight].value;
        TaggedObject taggedObject = ObjectManager.GetInstance().GetClothestObject(maxDistance, blob, enemies);

        if (taggedObject != null)
        {
            enemyToChase = taggedObject.gameObject;
            return 500.0F;
        }
        else
        {*/
            return -100.0F;
        //}
    }

    public void MakeDecision()
    {
        blobMovement.StartWandering();
    }

    public void PerformAction()
    {
        // Nothing
    }
}
