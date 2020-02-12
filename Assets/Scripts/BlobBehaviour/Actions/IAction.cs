using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    ///> Higher score means more likely to perform certain action, 0 is neutral
    float GetActionPriorityScore();
    void PerformAction();
    void MakeDecision();
}
