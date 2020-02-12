using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EatAction : IAction
{
    private readonly GameObject blob;
    private readonly List<List<ObjectTag>> EdibleTagCombinations;

    private readonly Energy energy;
    private readonly BlobMovement blobMovement;
    private BlobStats blobStats;

    GameObject food;
    bool isWandering = false;

    public EatAction(GameObject blob, List<List<ObjectTag>> edibleTagCombinations)
    {
        this.blob = blob;
        energy = blob.GetComponent<Energy>();
        blobMovement = blob.GetComponent<BlobMovement>();
        EdibleTagCombinations = edibleTagCombinations;
        blobStats = blob.GetComponent<BlobStats>();
    }

    public float GetActionPriorityScore() // 0.0 - 1000.0F 
    {
        return Math.Min(1000.0F, 1000.0F * (1.0F - energy.GetEnergy() / blobStats.stats.stats[StatName.MaxEnergy].value));
    }

    public void PerformAction()
    {
        if(food != null)
        {
            Vector3 colliderPosition = blob.transform.position;
            colliderPosition.y += blob.transform.localScale.y / 2;
            Collider[] hitColliders = Physics.OverlapSphere(colliderPosition, 0.25F, 0x0100); // 0x0100 is layermask for layer 8

            if (hitColliders.Any(x => x.gameObject.GetInstanceID() == food.GetInstanceID()))
            {
                energy.AddEnergy(food.GetComponent<Edible>().Bite(1));
                food = null;
                blobMovement.Stop();
            }
        }
        else if (!isWandering)
        {
            blobMovement.Stop();
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
        TaggedObject taggedObject = ObjectManager.GetInstance().GetClothestObject(maxDistance, blob, foodCandidates);

        if(taggedObject != null)
        {
            food = taggedObject.gameObject;
            blobMovement.RunTo(food.transform.position);
            isWandering = false;
        }
        else
        {
            blobMovement.StartWandering();
            isWandering = true;
        }
    }
}
