using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelGoal
{
    bool IsRequirementMet();
    string GetGoalDescription(); // TODO to explain goals
}
