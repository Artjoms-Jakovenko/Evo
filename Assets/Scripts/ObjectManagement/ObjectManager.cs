using System;
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
            if (taggedObject.ObjectTagList.Any(x => objectTags.Contains(x)))
            {
                taggedObjects.Add(taggedObject);
            }
        }
        return taggedObjects;
    }

    public List<TaggedObject> GetAllEnemies(TeamTag ownTeamTag)
    {
        List<TaggedObject> taggedObjects = new List<TaggedObject>();

        List<TeamTag> teamTags = Enum.GetValues(typeof(TeamTag)).Cast<TeamTag>().ToList();
        teamTags.Remove(ownTeamTag);
        teamTags.Remove(TeamTag.None);

        foreach (var taggedObject in TaggedObjects)
        {
            if (teamTags.Contains(taggedObject.teamTag))
            {
                taggedObjects.Add(taggedObject);
            }
        }
        return taggedObjects;
    }

    public List<TaggedObject> GetAllTeammates(TeamTag teamTag)
    {
        List<TaggedObject> taggedObjects = new List<TaggedObject>();

        foreach (var taggedObject in TaggedObjects)
        {
            if (taggedObject.teamTag == teamTag)
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

    public void AddObject(GameObject objectToAdd)
    {
        GameObjectUtility.MakeChild(objectToAdd, gameObject);
        TaggedObjects.Add(objectToAdd.GetComponent<TaggedObject>());
    }

    public void DestroyObject(GameObject gameObject)
    {
        RemoveFromObjectList(gameObject);
        Destroy(gameObject);
    }

    public void DestroyObject(GameObject gameObject, float delay)
    {
        RemoveFromObjectList(gameObject);
        Destroy(gameObject, delay);
    }

    public void RemoveFromObjectList(GameObject gameObject)
    {
        TaggedObjects.RemoveAll(x => x.gameObject.GetInstanceID() == gameObject.GetInstanceID());
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
