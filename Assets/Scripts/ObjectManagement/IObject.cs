using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractObject : MonoBehaviour
{
    private static long _objectID = 0;
    public long objectID;
    public List<ObjectTag> ObjectTags { set; get; }
    public GameObject _gameObject { set; get; }

    public AbstractObject()
    {
        objectID = _objectID;
        _gameObject = gameObject;
        _objectID++;
    }
}
