using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAction : IAction // TODO leave for better times
{
    private BlobMovement blobMovement;

    public WanderAction(GameObject blob)
    {
        blobMovement = blob.GetComponent<BlobMovement>();
    }

    public float GetActionPriorityScore()
    {
        return 100;
    }

    public void MakeDecision()
    {
        blobMovement.StartWandering();
    }

    public void PerformAction()
    {
        // Nothing
    }
}
