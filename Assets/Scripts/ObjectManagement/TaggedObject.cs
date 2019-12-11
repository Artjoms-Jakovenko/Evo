using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaggedObject : MonoBehaviour
{
    public List<ObjectTag> ObjectTagList = new List<ObjectTag>();

    public void AddTag(ObjectTag tag)
    {
        if (!ObjectTagList.Contains(tag))
        {
            ObjectTagList.Add(tag);
        }
    }
}
