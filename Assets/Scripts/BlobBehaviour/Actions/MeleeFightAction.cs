using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeFightAction : IAction
{
    private readonly Transform blobTransform;
    private AnimationController blobAnimationController;
    private readonly BlobMovement blobMovement;
    private BlobStats blobStats; // Get rid of these in all actions

    public List<StatName> RequiredStats => throw new System.NotImplementedException();

    public List<Component> RequiredComponents => throw new System.NotImplementedException();

    GameObject enemyToChase;
    GameObject enemyToFight;
    List<TaggedObject> enemies;

    public MeleeFightAction(Transform blobTransform)
    {
        this.blobTransform = blobTransform;
        blobMovement = blobTransform.gameObject.GetComponent<BlobMovement>();
        blobAnimationController = blobTransform.gameObject.GetComponent<AnimationController>();
        blobStats = blobTransform.gameObject.GetComponent<BlobStats>();
    }

    public float GetActionPriorityScore()
    {
        enemies = ObjectManager.GetInstance().GetAllWithTags(new List<ObjectTag>() { ObjectTag.Vegetarian }); // TODO rebalance
        enemies.RemoveAll(x => x.gameObject.GetInstanceID() == blobTransform.gameObject.GetInstanceID()); // Remove hunting blob if present
        float maxDistance = blobStats.stats.stats[StatName.Sight].value;
        TaggedObject taggedObject  = ObjectManager.GetInstance().GetClothestObject(maxDistance, blobTransform.gameObject, enemies);
        
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
        
    }

    public void PerformAction()
    {
        if (enemyToChase != null)
        {
            Vector3 colliderPosition = blobTransform.position;
            colliderPosition.y += blobTransform.localScale.y / 2;
            Collider[] hitColliders = Physics.OverlapSphere(colliderPosition, blobTransform.localScale.x / 2.0F * 1.2F, 0x01); // TODO change layermask, change scale*/
            if (hitColliders.Any(x => x.gameObject.GetInstanceID() == enemyToChase.GetInstanceID()))
            {
                blobAnimationController.PlayAnimation(this);
                blobMovement.LookTo(blobTransform, enemyToChase.transform);
                enemyToFight = enemyToChase;
                enemyToChase = null;
            }
            else
            {
                blobAnimationController.PlayAnimation(AnimationState.Walk);
                blobMovement.RunAndLookTo(blobTransform, enemyToChase.transform);
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
