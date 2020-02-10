using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    private float speed;

    public delegate void SpeedChanged();
    public event SpeedChanged OnSpeedChanged;

    void Start()
    {
        SetSpeed(GetComponent<BlobStats>().stats.stats[StatName.Speed].value);
    }

    public void SetSpeed(float newSpeedValue)
    {
        speed = newSpeedValue;
        OnSpeedChanged();
    }

    public float GetSpeed()
    {
        return speed;
    }
}
