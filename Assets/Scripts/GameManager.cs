using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private List<Transform> availableSpawnPoints = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < spawnPointsParent.transform.childCount; i++)
        {
            availableSpawnPoints.Add(spawnPointsParent.transform.GetChild(i));
        }
    }

    private void Start()
    {
        
        // Init enemies
    }

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
                GameRewardsSystem.AdministerRewards();

                LevelManager.RecordLevelCompletion(SceneManager.GetActiveScene().name); // TODO add possibility to fail a level
            }
        }
    }

    public void StartRound()
    {
        List<int> selectedBlobIds = blobSelectorBar.GetSelectedBlobIds();
        blobSelector.SetActive(false);
        
        SaveData saveData = SaveSystem.Load();

        List<BlobStatsData> blobStatsDatas = new List<BlobStatsData>();
        foreach(int selectedBlobId in selectedBlobIds)
        {
            blobStatsDatas.Add(saveData.blobData[selectedBlobId]);
        }

        Spawn(blobStatsDatas, ObjectTag.PlayerTeam); // TODO other teams too

        Destroy(spawnPointsParent);
        Time.timeScale = 1.0F; // TODO manage timescale reset
        roundStarted = true;
    }

    private void Spawn(List<BlobStatsData> blobStatsDatas, ObjectTag teamName)
    {
        CheckIfEnoughSpawnPoints(blobStatsDatas.Count, availableSpawnPoints.Count);

        List<int> spawnPointOrder = Enumerable.Range(0, availableSpawnPoints.Count).OrderBy(x => Guid.NewGuid()).Take(blobStatsDatas.Count).ToList();

        for (int i = 0; i < blobStatsDatas.Count; i++)
        {
            GameObject blob = BlobInstantiator.GetBlobGameObject(blobStatsDatas[i], teamName);

            Transform spawnPoint = spawnPointsParent.transform.GetChild(spawnPointOrder[i]); // TODO
            blob.transform.localPosition = spawnPoint.localPosition;

            ObjectManager.GetInstance().AddObject(blob);
            availableSpawnPoints.RemoveAt(spawnPointOrder[i]);
        }
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
