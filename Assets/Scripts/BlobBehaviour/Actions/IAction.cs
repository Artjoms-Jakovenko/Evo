using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    List<Stats> RequiredStats { get; }
    List<Component> RequiredComponents { get; }
    ///> Higher score means more likely to perform certain action, 0 is neutral
    float GetActionPriorityScore();
    void PerformAction();
    void MakeDecision();
}
