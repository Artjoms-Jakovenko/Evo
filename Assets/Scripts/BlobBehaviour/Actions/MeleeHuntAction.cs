using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHuntAction : IAction
{
    private readonly Transform blobTransform;
    List<List<ObjectTag>> EdibleTagCombinations { get; set; }

    public List<StatName> RequiredStats => throw new System.NotImplementedException();

    public List<Component> RequiredComponents => throw new System.NotImplementedException();

    public MeleeHuntAction()
    {

    }

    public float GetActionPriorityScore()
    {
        return 600.0F; // TODO change dynamically, maybe randomly
    }

    public void MakeDecision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(blobTransform.position, blobTransform.localScale.x / 2, 0x01); // TODO change layermask, change scale
        throw new System.NotImplementedException();
    }

    public void PerformAction()
    {
        throw new System.NotImplementedException();
    }
}
