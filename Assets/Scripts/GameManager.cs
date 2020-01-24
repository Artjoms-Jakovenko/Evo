using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject objectsList;

    private void Start() // Turn into awake, Must fix, because objectmanager is null
    {
        SaveData blobStatsData = SaveSystem.Load();

        for (int i = 0; i < 10; i++)
        {
            GameObject blob = BlobInstantiator.GetBlobGameObject(blobStatsData.blobData[0]);

            blob.transform.position = new Vector3(10 - 2 * i, 0.0F, i);

            ObjectManager.GetInstance().AddObject(blob);
        }  
    }

    private void Update()
    {
        var li = ObjectManager.GetInstance().GetAllWithTags(new List<ObjectTag>() { ObjectTag.Vegetarian });
        if(li.Count == 0)
        {
            Debug.Log("Game over");
        }
    }
}
