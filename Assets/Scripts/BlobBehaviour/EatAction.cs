using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAction : IAction
{
    List<List<ObjectTag>> EdibleTagCombinations { get; set; }
    private Transform blobTransform;

    public EatAction(Transform blobTransform)
    {
        this.blobTransform = blobTransform;
    }

    public float GetActionPriorityScore()
    {
        return 1; // TODO
    }

    public void PerformAction()
    {
        blobTransform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));
    }

    public void MakeDecision()
    {
        // TODO
    }
}
