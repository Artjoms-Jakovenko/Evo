using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private ObjectManager()
    {

    }

    private List<TaggedObject> TaggedObjects = new List<TaggedObject>(); 

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
            _instance.TaggedObjects.Add(child.GetComponent<TaggedObject>());
        }
    }

    // AND behaviour
    public List<TaggedObject> GetAllWithTagCombination(List<ObjectTag> objectTags)
    {
        List<TaggedObject> taggedObjects = new List<TaggedObject>();
        foreach (var taggedObject in TaggedObjects)
        {
            //if (taggedObject.ObjectTagList.Contains(objectTags))
            if (!objectTags.Except(taggedObject.ObjectTagList).Any())
            {
                taggedObjects.Add(taggedObject);
            }
        }
        return taggedObjects;
    }

    // OR behaviour
    public List<TaggedObject> GetAllWithTags(List<ObjectTag> objectTags)
    {
        List<TaggedObject> taggedObjects = new List<TaggedObject>();
        foreach (var taggedObject in TaggedObjects)
        {
            //if (taggedObject.ObjectTagList.Contains(objectTags))
            if (taggedObject.ObjectTagList.Any(x => objectTags.Contains(x)))
            {
                taggedObjects.Add(taggedObject);
            }
        }
        return taggedObjects;
    }

    public void AddObject(GameObject objectToAdd)
    {
        GameObjectUtility.MakeChild(objectToAdd, gameObject);
        TaggedObjects.Add(objectToAdd.GetComponent<TaggedObject>());
    }

    public void DestroyObject(GameObject gameObject)
    {
        TaggedObjects.RemoveAll(x => x.gameObject.GetInstanceID() == gameObject.GetInstanceID());
        Destroy(gameObject);
    }
}
