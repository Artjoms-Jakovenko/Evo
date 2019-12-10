using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private ObjectManager()
    {

    }

    private List<ObjectTags> TaggedObjects = new List<ObjectTags>(); 

    private static ObjectManager _instance;

    public static ObjectManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ObjectManager();
        }
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        foreach (Transform child in transform)
        {
            _instance.TaggedObjects.Add(child.GetComponent<ObjectTags>());
        }
    }

    public void AddObject(ObjectTags anObject) // TODO rename
    {
        TaggedObjects.Add(anObject);
    }

    void Start()
    {
        
    }
}
