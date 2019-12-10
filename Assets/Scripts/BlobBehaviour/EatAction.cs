using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EatAction : IAction
{
    
    private Transform blobTransform;
    private Hunger hunger;
    List<List<ObjectTag>> EdibleTagCombinations { get; set; }

    Vector3 foodLocation;

    public EatAction(Transform blobTransform, List<List<ObjectTag>> edibleTagCombinations)
    {
        this.blobTransform = blobTransform;
        hunger = blobTransform.gameObject.GetComponent<Hunger>();
        EdibleTagCombinations = edibleTagCombinations;
    }

    public float GetActionPriorityScore()
    {
        return 95.0F - hunger.energy; // TODO
    }

    public void PerformAction()
    {
        Vector3 movement = (foodLocation - blobTransform.position).normalized; // TODO

        blobTransform.Translate(new Vector3(movement.x * Time.deltaTime, movement.y * Time.deltaTime, movement.z * Time.deltaTime));
    }

    public void MakeDecision()
    {
        List<TaggedObject> foodCandidates = new List<TaggedObject>();
        foreach (var edibleCombination in EdibleTagCombinations)
        {
            foodCandidates = foodCandidates.Union(ObjectManager.GetInstance().GetTagCombinations(edibleCombination)).ToList();
        }

        foodLocation = foodCandidates[0].transform.position;
    }
}
