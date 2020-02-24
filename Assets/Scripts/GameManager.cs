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
    public LevelInfo levelInfo;

    private LevelEnum currentLevel;
    private LevelGoalSystem levelGoalSystem;
    float roundTime = 60.0F;
    bool roundStarted;
    private readonly List<Transform> availableSpawnPoints = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < spawnPointsParent.transform.childCount; i++)
        {
            availableSpawnPoints.Add(spawnPointsParent.transform.GetChild(i));
        }
    }

    private void Start()
    {
        Time.timeScale = 0.0F;
        levelGoalSystem = GetComponent<LevelGoalSystem>();
        currentLevel = LevelManager.GetLevelEnum(SceneManager.GetActiveScene().name);
        List<BlobStatsData> enemiesData = LevelInfoData.GetLevelEnemies(currentLevel);
        Spawn(enemiesData, TeamTag.Enemy);
    }
    
    private void Update()
    {
        if (roundStarted)
        {
            roundTime -= Time.deltaTime;
            List<TaggedObject> playerBlobs = ObjectManager.GetInstance().GetAllTeammates(TeamTag.Player);
            if (playerBlobs.Count == 0 || roundTime <= 0.0F) // TODO add small delay to observe last player death
            {
                roundStarted = false;
                Time.timeScale = 0;
                Debug.Log("Game over");
                rewardScreen.SetActive(true);

                Debug.Log("Stars achieved: " + levelGoalSystem.GetLevelCompletedGoalCount()); // TODO remove and use in rewards

                if (roundTime > 0.0F)
                {
                    rewardScreen.GetComponent<RewardScreen>().AdministerRewards(currentLevel, 0); // TODO stars based on achievements
                }
                else
                {
                    rewardScreen.GetComponent<RewardScreen>().AdministerRewards(currentLevel, 3); // TODO stars based on achievements
                    LevelManager.RecordLevelCompletion(SceneManager.GetActiveScene().name, 3); // TODO add possibility to fail a level and stars on achievements
                }
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

        Spawn(blobStatsDatas, TeamTag.Player);

        Destroy(spawnPointsParent);
        Time.timeScale = 1.0F;
        roundStarted = true;
    }

    private void Spawn(List<BlobStatsData> blobStatsDatas, TeamTag teamTag)
    {
        CheckIfEnoughSpawnPoints(blobStatsDatas.Count, availableSpawnPoints.Count);

        for (int i = 0; i < blobStatsDatas.Count; i++)
        {
            GameObject blob = BlobInstantiator.GetBlobGameObject(blobStatsDatas[i], teamTag);

            System.Random randomNumberGenerator = new System.Random();
            int randomIndex = randomNumberGenerator.Next(0, availableSpawnPoints.Count);

            Transform spawnPoint = availableSpawnPoints[randomIndex];
            blob.transform.localPosition = spawnPoint.localPosition;

            ObjectManager.GetInstance().AddObject(blob);
            availableSpawnPoints.RemoveAt(randomIndex);
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
