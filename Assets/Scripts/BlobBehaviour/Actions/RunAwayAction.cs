using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RunAwayAction : IAction
{
    private readonly GameObject blob;
    private readonly BlobMovement blobMovement;
    private readonly TeamTag teamTag;
    private readonly BlobStats blobStats;

    private TaggedObject enemyToRunFrom;

    public RunAwayAction(GameObject blob)
    {
        this.blob = blob;
        blobMovement = blob.GetComponent<BlobMovement>();
        teamTag = blob.GetComponent<TaggedObject>().teamTag;
        blobStats = blob.GetComponent<BlobStats>();
    }

    public float GetActionPriorityScore() // TODO run only from fighters
    {
        List<TaggedObject> enemies = ObjectManager.GetInstance().GetAllEnemies(teamTag);
        enemies = enemies.Where(x => x.GetComponent<BlobStats>().stats.blobType == BlobType.Fighter).ToList();
        
        float maxDistance = blobStats.stats.stats[StatName.Sight].value;
        enemyToRunFrom = ObjectManager.GetInstance().GetClothestObject(maxDistance, blob, enemies);

        if (enemyToRunFrom != null)
        {
            float distanceToEnemy = Vector3.Distance(blob.transform.position, enemyToRunFrom.transform.position);
            return 1000.0F - distanceToEnemy * 100.0F; // TODO smarter runaway formula
        }
        else
        {
            return -100.0F;
        }
    }

    public void MakeDecision()
    {
        if (enemyToRunFrom != null)
        {
            blobMovement.RunAway(enemyToRunFrom.transform);
        }
    }

    public void PerformAction()
    {
        // Nothing
    }
}
