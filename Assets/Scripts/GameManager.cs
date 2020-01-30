using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject rewardScreen;
    public GameObject blobSelector;
    public BlobSelector blobSelectorBar;
    public GameObject spawnPointsParent;
    public LevelInfo levelInfo; // TODO remove if not used

    //public GameObject objectsList;
    float roundTime = 20.0F;
    bool roundStarted;

    private void Update()
    {
        if (roundStarted)
        {
            roundTime -= Time.deltaTime;
            var li = ObjectManager.GetInstance().GetAllWithTags(new List<ObjectTag>() { ObjectTag.Vegetarian });
            if (li.Count == 0 || roundTime <= 0.0F)
            {
                roundStarted = false;
                Time.timeScale = 0;
                Debug.Log("Game over");
                rewardScreen.SetActive(true);
                GameRewardsSystem.AdministerRewards(); // TODO get out of the loop with rewards
            }
        }
    }

    public void StartRound()
    {
        List<int> selectedBlobIds = blobSelectorBar.GetSelectedBlobIds();
        blobSelector.SetActive(false);
        
        SaveData saveData = SaveSystem.Load();

        int spawnPointCount = spawnPointsParent.transform.childCount;
        CheckIfEnoughSpawnPoints(selectedBlobIds.Count, spawnPointCount);

        List<int> spawnPointOrder = Enumerable.Range(0, spawnPointsParent.transform.childCount).OrderBy(x => Guid.NewGuid()).Take(selectedBlobIds.Count).ToList();

        for (int i = 0; i < selectedBlobIds.Count; i++)
        {
            GameObject blob = BlobInstantiator.GetBlobGameObject(saveData.blobData[selectedBlobIds[i]]);

            Transform spawnPoint = spawnPointsParent.transform.GetChild(spawnPointOrder[i]); // TODO
            blob.transform.localPosition = spawnPoint.localPosition;

            ObjectManager.GetInstance().AddObject(blob);
        }

        Destroy(spawnPointsParent);
        Time.timeScale = 1.0F; // TODO manage timescale reset
        roundStarted = true;
    }

    private bool CheckIfEnoughSpawnPoints(int blobCount, int spawnPointCount)
    {
        if (blobCount > spawnPointCount) // TODO make it a test
        {
            Debug.LogError("Not enough spawn points");
            return false;
        }
        return true;
    }
}
