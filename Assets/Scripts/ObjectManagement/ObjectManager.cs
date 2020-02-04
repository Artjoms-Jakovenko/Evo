using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private readonly List<TaggedObject> TaggedObjects = new List<TaggedObject>(); 

    private static ObjectManager _instance;

    public static ObjectManager GetInstance()
    {
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

    public void RemoveWithTag(List<TaggedObject> taggedObjects, ObjectTag removalTag)
    {
        taggedObjects.RemoveAll(x => x.ObjectTagList.Contains(removalTag));
    }

    public ObjectTag GetObjectTeamTag(TaggedObject taggedObject)
    {
        if (taggedObject.ObjectTagList.Contains(ObjectTag.PlayerTeam))
        {
            return ObjectTag.PlayerTeam;
        } 
        else if (taggedObject.ObjectTagList.Contains(ObjectTag.EnemyTeam))
        {
            return ObjectTag.EnemyTeam;
        }
        else
        {
            Debug.LogError("Could not locate team tag");
            throw new System.Exception();
        }
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

    public TaggedObject GetClothestObject(float maxDistance, GameObject origin, List<TaggedObject> objects)
    {
        TaggedObject closestObject = null;
        foreach (var taggedObject in objects)
        {
            float objectDistance = Vector3.Distance(taggedObject.transform.position, origin.transform.position);
            if (objectDistance <= maxDistance)
            {
                maxDistance = objectDistance;
                closestObject = taggedObject;
            }
        }
        return closestObject;
    }
}
