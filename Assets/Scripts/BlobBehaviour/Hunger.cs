using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    private Energy energy;

    private void Start()
    {
        energy = GetComponent<Energy>();
    }

    private void Update()
    {
        energy.AddEnergy(-Time.deltaTime);
    }
}
