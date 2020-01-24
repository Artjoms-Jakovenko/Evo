using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneAction : IAction // TODO add animation idle
{
    private AnimationController blobAnimationController;

    public List<StatName> RequiredStats { get; } = new List<StatName>();
    public List<Component> RequiredComponents { get; } = new List<Component>();

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
