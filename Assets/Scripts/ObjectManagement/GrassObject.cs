using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassObject : AbstractObject
{
    private void Awake()
    {
        ObjectTags = new List<ObjectTag> { ObjectTag.Small, ObjectTag.Edible, ObjectTag.Plant };
    }
}
