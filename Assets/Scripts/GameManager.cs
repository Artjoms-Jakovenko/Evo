using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject objectsList;

    private void Start() // Turn into awake, Must fix, because objectmanager is null
    {
        SaveData blobStatsData = SaveSystem.Load();

        GameObject blob = BlobInstantiator.GetBlobGameObject(blobStatsData.blobData[0]);

        blob.transform.position = new Vector3(0.0F, 0.0F, 0.0F);

        ObjectManager.GetInstance().AddObject(blob);
    }
}
