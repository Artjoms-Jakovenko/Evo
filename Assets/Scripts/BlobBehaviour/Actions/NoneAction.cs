using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneAction : IAction
{
    public List<Stats> RequiredStats { get; } = new List<Stats>();
    public List<Component> RequiredComponents { get; } = new List<Component>();

    public float GetActionPriorityScore()
    {
        return 0;
    }

    public void MakeDecision()
    {
        // Do nothing
    }

    public void PerformAction()
    {
        // Do nothing
    }
}
