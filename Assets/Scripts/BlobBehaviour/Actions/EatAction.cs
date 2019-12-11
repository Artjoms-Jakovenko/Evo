using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EatAction : IAction
{
    
    private readonly Transform blobTransform;
    private readonly Hunger hunger;
    private readonly BlobMovement blobMovement;
    List<List<ObjectTag>> EdibleTagCombinations { get; set; }
    public List<Stats> RequiredStats { get; } = new List<Stats>() { Stats.Speed };
    public List<Component> RequiredComponents { get; } = new List<Component>() { Component.Hunger, Component.BlobMovement };

    GameObject food; // TODO implement eating based on collision

    public EatAction(Transform blobTransform, List<List<ObjectTag>> edibleTagCombinations)
    {
        this.blobTransform = blobTransform;
        hunger = blobTransform.gameObject.GetComponent<Hunger>();
        blobMovement = blobTransform.gameObject.GetComponent<BlobMovement>();
        EdibleTagCombinations = edibleTagCombinations;
    }

    public float GetActionPriorityScore()
    {
        return 95.0F - hunger.energy; // TODO
    }

    public void PerformAction()
    {
        if(food != null)
        {
            Collider[] hitColliders = Physics.OverlapBox(blobTransform.position, blobTransform.localScale / 2, Quaternion.identity, 0x01); // TODO change layermask

            if (hitColliders.Any(x => x.gameObject.GetInstanceID() == food.GetInstanceID()))
            {
                ObjectManager.GetInstance().DestroyObject(food);
                food = null;
            }
            else
            {
                blobMovement.RunAndLookTo(blobTransform, food.transform);
            }
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
