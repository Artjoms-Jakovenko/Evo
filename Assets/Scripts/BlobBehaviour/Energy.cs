﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private float energy;
    private float maxEnergy;
    private Health health;

    private void Start()
    {
        maxEnergy = GetComponent<BlobStats>().stats.stats[StatName.MaxEnergy].value;
        energy = maxEnergy / 2.0F;
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if(energy <= 0.0F)
        {
            health.TakeDamage(Math.Abs(Time.deltaTime * energy));
        }
    }

    public float GetEnergy()
    {
        return energy;
    }

    public void AddEnergy(float energyAmount)
    {
        energy += energyAmount;

        if(energy >= maxEnergy)
        {
            energy = maxEnergy;
        }
    }
}
