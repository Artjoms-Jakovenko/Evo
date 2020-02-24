using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelGoal
{
    bool IsRequirementMet();
    string GoalDescription(); // TODO to explain goals
}
