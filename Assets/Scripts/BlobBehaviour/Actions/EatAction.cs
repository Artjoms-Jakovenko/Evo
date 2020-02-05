using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EatAction : IAction
{
    
    private readonly Transform blobTransform;
    private readonly Energy energy;
    private readonly BlobMovement blobMovement;
    private AnimationController blobAnimationController;
    List<List<ObjectTag>> EdibleTagCombinations;
    public List<StatName> RequiredStats { get; } = new List<StatName>() { StatName.Speed, StatName.MaxEnergy };
    public List<Component> RequiredComponents { get; } = new List<Component>() { Component.Hunger, Component.BlobMovement };

    private BlobStats blobStats;

    GameObject food;

    public EatAction(Transform blobTransform, List<List<ObjectTag>> edibleTagCombinations)
    {
        this.blobTransform = blobTransform;
        energy = blobTransform.gameObject.GetComponent<Energy>();
        blobMovement = blobTransform.gameObject.GetComponent<BlobMovement>();
        EdibleTagCombinations = edibleTagCombinations;
        blobStats = blobTransform.gameObject.GetComponent<BlobStats>();
        blobAnimationController = blobTransform.gameObject.GetComponent<AnimationController>();
    }

    public float GetActionPriorityScore() // 0.0 - 1000.0F 
    {
        return Math.Min(1000.0F, 1000.0F * (1.0F - energy.GetEnergy() / blobStats.stats.stats[StatName.MaxEnergy].value));
    }

    public void PerformAction()
    {
        if(food != null)
        {
            Vector3 colliderPosition = blobTransform.position;
            colliderPosition.y += blobTransform.localScale.y / 2;
            Collider[] hitColliders = Physics.OverlapSphere(colliderPosition, 0.5F, 0x0100); // 0x0100 is layermask for layer 8

            if (hitColliders.Any(x => x.gameObject.GetInstanceID() == food.GetInstanceID()))
            {
                energy.AddEnergy(food.GetComponent<Edible>().Bite(1));
                food = null;
                blobAnimationController.PlayAnimation(AnimationState.Idle); // Presumably this doesnt work
            }
            else
            {
                blobAnimationController.PlayAnimation(AnimationState.Walk);
                blobMovement.RunAndLookTo(blobTransform, food.transform);
            }
        }
        else
        {
            blobAnimationController.PlayAnimation(AnimationState.Idle);
        }
    }

    public void MakeDecision()
    {
        List<TaggedObject> foodCandidates = new List<TaggedObject>();
        foreach (var edibleCombination in EdibleTagCombinations)
        {
            foodCandidates = foodCandidates.Union(ObjectManager.GetInstance().GetAllWithTagCombination(edibleCombination)).ToList();
        }

        float maxDistance = blobStats.stats.stats[StatName.Sight].value;
        TaggedObject taggedObject = ObjectManager.GetInstance().GetClothestObject(maxDistance, blobTransform.gameObject, foodCandidates);

        if(taggedObject != null)
        {
            food = taggedObject.gameObject;
        }
        else
        {
            // TODO implement wandering for food
        }
    }
}
