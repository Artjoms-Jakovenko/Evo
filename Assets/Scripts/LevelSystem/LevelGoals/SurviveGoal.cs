using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveGoal : ILevelGoal
{
    float neededSurviveTime;

    public SurviveGoal(float neededSurviveTime)
    {
        this.neededSurviveTime = neededSurviveTime;
    }

    public bool IsRequirementMet()
    {
        //if(ObjectManager.GetInstance().GetAllTeammates(TeamTag.Player).Count > 0 && Time.time >= neededSurviveTime) // TODO left out to check if any of the blobs are alive
        if(Time.timeSinceLevelLoad >= neededSurviveTime)
        {
            return true;
        }
        return false;
    }

    public string GetGoalDescription()
    {
        return "Survive for " + neededSurviveTime + " seconds";
    }
}
