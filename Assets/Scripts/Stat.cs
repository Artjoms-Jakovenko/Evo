using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat<T>
{
    public T value;
    public T maxValue;

    public Stat(T value, T maxValue)
    {
        this.value = value;
        this.maxValue = maxValue;
    }
}
