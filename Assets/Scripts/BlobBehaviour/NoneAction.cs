using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneAction : IAction
{
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
