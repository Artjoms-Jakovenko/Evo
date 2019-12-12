using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlobStats : MonoBehaviour
{
    // Make into properties to assign buffs
    public Stat Speed = new Stat(1.0F, 1.0F, 2.0F, 0, 4);
    public Stat EnergyLimit = new Stat(20.0F, 20.0F, 40.0F, 0, 4);
    public Stat Health = new Stat(1.0F, 1.0F, 2.0F, 0, 4);
    //public Stat<float> StartingEnergy = new Stat<float>(10.0F, 10.0F, 50.0F, 4);
    public Stat Sight = new Stat(5.0F, 5.0F, 10.0F, 0, 4);
    public Stat ReactionTime = new Stat(3.0F, 3.0F, 2.0F, 0, 4);
    public Stat Strength = new Stat(1.0F, 1.0F, 2.0F, 0, 4);

    public int maxXPLevel = 3;

    //public bool? canBeHungry;
    //public bool? canRun;
}
