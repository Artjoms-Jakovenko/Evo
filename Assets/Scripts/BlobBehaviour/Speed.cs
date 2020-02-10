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

    private void Update()
    {
        if(Time.time > 5.0F)
        {
            if (!gameObject.GetComponent<BlobStats>().stats.possibleActions.Contains(Action.MeleeFight))
            {
                SetSpeed(10.0F);
            }
        }
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
