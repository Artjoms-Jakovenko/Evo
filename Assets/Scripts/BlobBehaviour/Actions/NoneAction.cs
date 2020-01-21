using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneAction : IAction // TODO add animation idle
{
    public List<StatName> RequiredStats { get; } = new List<StatName>();
    public List<Component> RequiredComponents { get; } = new List<Component>();

    public float GetActionPriorityScore()
    {
        return 500;
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
