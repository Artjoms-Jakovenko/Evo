using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EatAction : IAction
{
    
    private readonly Transform blobTransform;
    private readonly Hunger hunger;
    private readonly BlobMovement blobMovement;
    private Animator blobAnimator;
    List<List<ObjectTag>> EdibleTagCombinations { get; set; }
    public List<StatName> RequiredStats { get; } = new List<StatName>() { StatName.Speed, StatName.MaxEnergy };
    public List<Component> RequiredComponents { get; } = new List<Component>() { Component.Hunger, Component.BlobMovement };

    private BlobStats blobStats;

    GameObject food;

    public EatAction(Transform blobTransform, List<List<ObjectTag>> edibleTagCombinations)
    {
        this.blobTransform = blobTransform;
        hunger = blobTransform.gameObject.GetComponent<Hunger>();
        blobMovement = blobTransform.gameObject.GetComponent<BlobMovement>();
        EdibleTagCombinations = edibleTagCombinations;
        blobStats = blobTransform.gameObject.GetComponent<BlobStats>();
        blobAnimator = blobTransform.gameObject.GetComponent<Animator>();
    }

    public float GetActionPriorityScore()
    {
        return 1000.0F * (1.0F - hunger.energy / blobStats.stats.stats[StatName.MaxEnergy].value);
    }

    public void PerformAction()
    {
        if(food != null)
        {
            Collider[] hitColliders = Physics.OverlapBox(blobTransform.position, blobTransform.localScale / 2, Quaternion.identity, 0x01); // TODO change layermask

            if (hitColliders.Any(x => x.gameObject.GetInstanceID() == food.GetInstanceID()))
            {
                hunger.energy += food.GetComponent<Edible>().Bite(1);
                if(hunger.energy > blobStats.stats.stats[StatName.MaxEnergy].value)
                {
                    hunger.energy = blobStats.stats.stats[StatName.MaxEnergy].value;
                }
                food = null;
                blobAnimator.SetTrigger("GoIdle"); // TODO structure
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
