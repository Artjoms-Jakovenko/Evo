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
    private Energy energy;

    readonly TeamTag teamTag;
    GameObject enemyToChase;
    GameObject enemyToFight;
    List<TaggedObject> enemies;
    bool isWandering = false;

    public MeleeFightAction(GameObject blob)
    {
        this.blob = blob;
        blobMovement = blob.GetComponent<BlobMovement>();
        blobAnimationController = blob.GetComponent<AnimationController>();
        blobStats = blob.GetComponent<BlobStats>();
        teamTag = blob.GetComponent<TaggedObject>().teamTag;
        energy = blob.GetComponent<Energy>();
        blobAnimationController.OnKicked += DealDamage;
    }

    public float GetActionPriorityScore()
    {
        enemies = ObjectManager.GetInstance().GetAllWithTags(new List<ObjectTag>() { ObjectTag.Vegetarian });

        float maxDistance = blobStats.stats.stats[StatName.Sight].value;
        TaggedObject taggedObject  = ObjectManager.GetInstance().GetClothestObject(maxDistance, blob, enemies);
        
        if(taggedObject == null)
        {
            enemyToChase = null;
        }
        else
        {
            enemyToChase = taggedObject.gameObject;
        }
        return 0.6F - energy.GetEnergy() / 20.0F;
    }

    public void MakeDecision()
    {
        if (enemyToChase != null)
        {
            blobAnimationController.PlayAnimation(AnimationState.Walk);
            blobMovement.RunTo(enemyToChase.transform.position); // TODO move outside the loop
        }
        else
        {
            blobMovement.StartWandering();
            isWandering = true;
        }
    }

    public void PerformAction()
    {
        if (enemyToChase != null)
        {
            Vector3 colliderPosition = blob.transform.position;
            colliderPosition.y += blob.transform.localScale.y / 2;
            Collider[] hitColliders = Physics.OverlapSphere(colliderPosition, 0.9F * 1.3F, 0x0100);
            if (hitColliders.Any(x => x.gameObject.GetInstanceID() == enemyToChase.GetInstanceID()))
            {
                blobAnimationController.PlayAnimation(AnimationState.Kick);
                blobMovement.LookTo(blob.transform, enemyToChase.transform); // TODO remove
                enemyToFight = enemyToChase;
                enemyToChase = null;
                blobMovement.Stop();
            }
            isWandering = false;
        }
    }

    public void DealDamage()
    {
        if (enemyToFight != null)
        {
            enemyToFight.GetComponent<Health>().TakeDamage(1);
            energy.AddEnergy(20.0F);
        }
        else
        {
            Debug.Log("Enemy was null. Could not kick.");
            if (!isWandering)
            {
                blobMovement.Stop();
            }
        }
    }
}
