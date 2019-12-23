using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlobStatsData
{
    // Make into properties to assign buffs
    public Stat Speed = new Stat(1.0F, 1.0F, 2.0F, 0, 4, Stats.Speed);
    public Stat EnergyLimit = new Stat(20.0F, 20.0F, 40.0F, 0, 4, Stats.MaxEnergy);
    public Stat Health = new Stat(1.0F, 1.0F, 2.0F, 0, 4, Stats.Health);
    //public Stat<float> StartingEnergy = new Stat<float>(10.0F, 10.0F, 50.0F, 4);
    public Stat Sight = new Stat(5.0F, 5.0F, 10.0F, 0, 4, Stats.Sight);
    public Stat ReactionTime = new Stat(3.0F, 3.0F, 2.0F, 0, 4, Stats.ReactionTime);
    public Stat Strength = new Stat(1.0F, 1.0F, 2.0F, 0, 4, Stats.Strength);

    public int maxXPLevel = 3;

    public List<Action> possibleActions = new List<Action>() { Action.Eat, Action.None };

    List<List<ObjectTag>> edibleTagCombinations = new List<List<ObjectTag>>()
        {
            new List<ObjectTag>(){ ObjectTag.Edible, ObjectTag.Small, ObjectTag.Plant }
        };

    //public bool? canBeHungry;
    //public bool? canRun;
}
