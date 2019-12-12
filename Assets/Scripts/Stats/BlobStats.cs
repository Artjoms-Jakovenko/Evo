using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlobStats : MonoBehaviour
{
    List<Stats> blobStats = new List<Stats>();

    public Stat<float> Speed = new Stat<float>(1.0F, 1.0F, 2.0F, 0, 4);
    public Stat<float> EnergyLimit = new Stat<float>(20.0F, 20.0F, 40.0F, 0, 4);
    public Stat<float> Health = new Stat<float>(1.0F, 1.0F, 2.0F, 0, 4);
    //public Stat<float> StartingEnergy = new Stat<float>(10.0F, 10.0F, 50.0F, 4);
    public Stat<float> Sight = new Stat<float>(5.0F, 5.0F, 10.0F, 0, 4);
    public Stat<float> ReactionTime = new Stat<float>(3.0F, 3.0F, 2.0F, 0, 4);
    public Stat<float> Strength = new Stat<float>(1.0F, 1.0F, 2.0F, 0, 4);

    public int maxXPLevel = 3;

    //public bool? canBeHungry;
    //public bool? canRun;
}
