using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeFightAction : IAction
{
    private readonly GameObject blob;
    private readonly AnimationController blobAnimationController;
    private readonly BlobMovement blobMovement;
    private readonly BlobStats blobStats; // Get rid of these in all actions

    public List<StatName> RequiredStats => throw new System.NotImplementedException();

    public List<Component> RequiredComponents => throw new System.NotImplementedException();

    readonly TeamTag teamTag;
    GameObject enemyToChase;
    GameObject enemyToFight;
    List<TaggedObject> enemies;

    public MeleeFightAction(GameObject blob)
    {
        this.blob = blob;
        blobMovement = blob.GetComponent<BlobMovement>();
        blobAnimationController = blob.GetComponent<AnimationController>();
        blobStats = blob.GetComponent<BlobStats>();
        teamTag = blob.GetComponent<TaggedObject>().teamTag;
    }

    public float GetActionPriorityScore()
    {
        enemies = ObjectManager.GetInstance().GetAllEnemies(teamTag);
        enemies.RemoveAll(x => x.gameObject.GetInstanceID() == blob.GetInstanceID()); // Remove hunting blob if present

        float maxDistance = blobStats.stats.stats[StatName.Sight].value;
        TaggedObject taggedObject  = ObjectManager.GetInstance().GetClothestObject(maxDistance, blob, enemies);
        
        if (taggedObject != null)
        {
            enemyToChase = taggedObject.gameObject;
            return 500.0F;
        }
        else
        {
            return -100.0F;
        }
    }

    public void MakeDecision()
    {
        if (enemyToChase != null)
        {
            blobAnimationController.PlayAnimation(AnimationState.Walk);
            blobMovement.RunTo(enemyToChase.transform.position); // TODO move outside the loop
        }
    }

    public void PerformAction()
    {
        if (enemyToChase != null)
        {
            Vector3 colliderPosition = blob.transform.position;
            colliderPosition.y += blob.transform.localScale.y / 2;
            Collider[] hitColliders = Physics.OverlapSphere(colliderPosition, 0.5F * 1.3F, 0x0100);
            if (hitColliders.Any(x => x.gameObject.GetInstanceID() == enemyToChase.GetInstanceID()))
            {
                blobAnimationController.PlayAnimation(this);
                blobMovement.LookTo(blob.transform, enemyToChase.transform); // TODO remove
                enemyToFight = enemyToChase;
                enemyToChase = null;
                blobMovement.Stop();
            }
        }
        else
        {
            blobAnimationController.PlayAnimation(AnimationState.Idle);
        }
    }

    public void DealDamage()
    {
        if (enemyToFight != null)
        {
            enemyToFight.GetComponent<Health>().TakeDamage(1);
        }
        else
        {
            Debug.Log("Enemy was null. Could not kick.");
        }
    }
}
