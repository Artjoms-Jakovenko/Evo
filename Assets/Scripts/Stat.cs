using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat<T>
{
    public T value;
    public T minValue;
    public T maxValue;

    public Stat(T value, T minValue, T maxValue)
    {
        this.value = value;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }
}
