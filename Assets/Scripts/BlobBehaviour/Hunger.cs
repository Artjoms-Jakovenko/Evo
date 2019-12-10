using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public float energy = 0;

    private void Update()
    {
        energy -= Time.deltaTime;
    }
}
