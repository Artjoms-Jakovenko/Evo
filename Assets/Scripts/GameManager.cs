using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject rewardScreen;

    //public GameObject objectsList;
    float roundTime = 20.0F;

    private void Start() // Turn into awake, Must fix, because objectmanager is null
    {
        SaveData blobStatsData = SaveSystem.Load();

        for (int i = 0; i < 10; i++)
        {
            GameObject blob = BlobInstantiator.GetBlobGameObject(blobStatsData.blobData[i % 2]);

            blob.transform.position = new Vector3(10 - 2 * i, 0.0F, -i);

            ObjectManager.GetInstance().AddObject(blob);
        }  
    }

    private void Update()
    {
        roundTime -= Time.deltaTime;
        var li = ObjectManager.GetInstance().GetAllWithTags(new List<ObjectTag>() { ObjectTag.Vegetarian });
        if(li.Count == 0 || roundTime <= 0.0F)
        {
            Time.timeScale = 0;
            Debug.Log("Game over");
            rewardScreen.SetActive(true);
            GameRewardsSystem.AdministerRewards(); // TODO get out of the loop with rewards
        }
    }
}
