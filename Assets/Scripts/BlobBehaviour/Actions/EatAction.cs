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
    private Animator blobAnimator;
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
        blobAnimator = blobTransform.gameObject.GetComponent<Animator>();
    }

    public float GetActionPriorityScore() // 0.0 - 1000.0F 
    {
        return Math.Max(1000.0F, 1000.0F * (1.0F - energy.GetEnergy() / blobStats.stats.stats[StatName.MaxEnergy].value));
    }

    public void PerformAction()
    {
        if(food != null)
        {
            Vector3 colliderPosition = blobTransform.position;
            colliderPosition.y += blobTransform.localScale.y / 2;
            Collider[] hitColliders = Physics.OverlapSphere(colliderPosition, blobTransform.localScale.x / 2, 0x01); // TODO change layermask, change scale

            if (hitColliders.Any(x => x.gameObject.GetInstanceID() == food.GetInstanceID()))
            {
                energy.AddEnergy(food.GetComponent<Edible>().Bite(1));
                food = null;
                blobAnimator.SetTrigger("GoIdle");
            }
            else
            {
                blobAnimator.SetTrigger("StartWalking");
                blobMovement.RunAndLookTo(blobTransform, food.transform);
            }
        }
        else
        {
            blobAnimator.SetTrigger("GoIdle");
        }
    }

    public void MakeDecision()
    {
        List<TaggedObject> foodCandidates = new List<TaggedObject>();
        foreach (var edibleCombination in EdibleTagCombinations)
        {
            foodCandidates = foodCandidates.Union(ObjectManager.GetInstance().GetAllWithTagCombination(edibleCombination)).ToList();
        }

        float shortestDistance = float.MaxValue;
        foreach (var foodCandidate in foodCandidates)
        {
            float foodDistance = Vector3.Distance(foodCandidate.transform.position, blobTransform.position); // TODO look into navmesh distance
            if (foodDistance < shortestDistance) 
            {
                shortestDistance = foodDistance;
                food = foodCandidate.gameObject;
            }
        }
    }
}
