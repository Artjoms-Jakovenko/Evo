using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneAction : IAction
{
    private AnimationController blobAnimationController;

    public NoneAction(GameObject blob)
    {
        blobAnimationController = blob.GetComponent<AnimationController>();
    }

    public float GetActionPriorityScore()
    {
        return 0;
    }

    public void MakeDecision()
    {
        blobAnimationController.PlayAnimation(AnimationState.Idle);
    }

    public void PerformAction()
    {
        // Do nothing
    }
}
