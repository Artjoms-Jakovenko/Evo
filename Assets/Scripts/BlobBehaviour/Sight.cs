using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    private float sight;

    private void Start()
    {
        sight = GetComponent<BlobStats>().stats.stats[StatName.Sight].value;
    }

    public float GetSight()
    {
        return sight;
    }
}
