using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlobStats : MonoBehaviour
{
    public Dictionary<Stats, Stat<float>> stats = new Dictionary<Stats, Stat<float>>();

    public void Awake()
    {
        stats.Add(Stats.Speed, new Stat<float>(1.0F, 1.0F, 5.0F));
    }

    public float? speed = 1.0F;
    public float? reactionTime = 1.0F;

    public bool? canBeHungry;
    public bool? canRun;
}
