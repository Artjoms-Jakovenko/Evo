using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeFightAction : IAction
{
    private readonly Transform blobTransform;
    private AnimationController blobAnimationController;
    private readonly BlobMovement blobMovement;
    private BlobStats blobStats; // Get rid of these in all actions

    public List<StatName> RequiredStats => throw new System.NotImplementedException();

    public List<Component> RequiredComponents => throw new System.NotImplementedException();

    GameObject enemy;

    public MeleeFightAction(Transform blobTransform)
    {
        this.blobTransform = blobTransform;
        blobMovement = blobTransform.gameObject.GetComponent<BlobMovement>();
        blobAnimationController = blobTransform.gameObject.GetComponent<AnimationController>();
        blobStats = blobTransform.gameObject.GetComponent<BlobStats>();
    }

    public float GetActionPriorityScore()
    {
        return 600.0F; // TODO change dynamically, maybe randomly
    }

    public void MakeDecision()
    {
        List<TaggedObject> enemies = ObjectManager.GetInstance().GetAllWithTags(new List<ObjectTag>() { ObjectTag.Vegetarian }); // TODO rebalance
        enemies.RemoveAll(x => x.gameObject.GetInstanceID() == blobTransform.gameObject.GetInstanceID()); // Remove hunting blob if present

        float maxDistance = blobStats.stats.stats[StatName.Sight].value;
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, blobTransform.position); // TODO look into navmesh distance
            if(distance <= maxDistance)
            {
                maxDistance = distance;
                this.enemy = enemy.gameObject;
            }
        }
    }

    bool once = true;

    public void PerformAction()
    {
        if (enemy != null)
        {
            Vector3 colliderPosition = blobTransform.position;
            colliderPosition.y += blobTransform.localScale.y / 2;
            Collider[] hitColliders = Physics.OverlapSphere(colliderPosition, blobTransform.localScale.x / 2.0F * 1.2F, 0x01); // TODO change layermask, change scale*/
            if (hitColliders.Any(x => x.gameObject.GetInstanceID() == enemy.GetInstanceID()))
            {
                blobAnimationController.PlayAnimation(AnimationState.Kick);
                enemy = null;
            }
            else
            {
                blobAnimationController.PlayAnimation(AnimationState.Walk);
                blobMovement.RunAndLookTo(blobTransform, enemy.transform);
            }
        }
        else
        {
            blobAnimationController.PlayAnimation(AnimationState.Idle);
        }
    }
}
