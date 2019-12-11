using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlobStats : MonoBehaviour
{
    List<Stats> blobStats = new List<Stats>();

    public Stat<float> Speed = new Stat<float>(1.0F, 5.0F);
    public Stat<float> EnergyLimit = new Stat<float>(100.0F, 120.0F);

    public float? reactionTime = 1.0F;

    public bool? canBeHungry;
    public bool? canRun;
}
