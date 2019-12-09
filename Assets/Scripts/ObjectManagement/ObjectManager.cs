using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectManager
{
    static List<AbstractObject> Objects = new List<AbstractObject>();

    public static void AddObject(AbstractObject anObject)
    {
        Objects.Add(anObject);
    }
}
