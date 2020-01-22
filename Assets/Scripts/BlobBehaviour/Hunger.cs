using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    private Energy energy;
    //private float hungerIntensity; // TODO add hunger intensity

    private void Awake()
    {
        energy = GetComponent<Energy>();
    }

    private void Update()
    {
        energy.AddEnergy(-Time.deltaTime);
    }
}
