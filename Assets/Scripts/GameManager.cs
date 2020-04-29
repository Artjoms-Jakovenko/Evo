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
    float afterDeathDelay = 1.25F;
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
        foreach (var enemyStatsData in enemiesData)
        {
            Spawn(enemyStatsData, TeamTag.Enemy, Vector3.zero); // TODO
        }
    }
    
    private void Update()
    {
        if (roundStarted)
        {
            roundTime -= Time.deltaTime;
            List<TaggedObject> playerBlobs = ObjectManager.GetInstance().GetAllTeammates(TeamTag.Player);
            if (playerBlobs.Count == 0 || roundTime <= 0.0F) // TODO add small delay to observe last player death
            {
                if(afterDeathDelay <= 0 || roundTime <= 0.0F)
                {
                    roundStarted = false;
                    Time.timeScale = 0;
                    Debug.Log("Game over");

                    Debug.Log("Stars achieved: " + levelGoalSystem.GetLevelCompletedGoalCount()); // TODO remove and use in rewards

                    #region strict order
                    int starsAchieved = 0;
                    bool levelCompleted = levelGoalSystem.IsLevelCompleted();

                    if (levelCompleted)
                    {
                        starsAchieved = levelGoalSystem.GetLevelCompletedGoalCount();
                    }

                    rewardScreen.GetComponent<RewardScreen>().AdministerRewards(currentLevel, levelCompleted, starsAchieved); // Must be before RecordLevelCompletion because it uses last star count
                    LevelManager.RecordLevelCompletion(SceneManager.GetActiveScene().name, levelCompleted, starsAchieved); // TODO add possibility to fail a level and stars on achievements
                    
                    rewardScreen.SetActive(true);
                    rewardScreen.GetComponent<RewardScreen>().RenderRewardScreen();
                    #endregion
                    //}
                }
                else
                {
                    afterDeathDelay -= Time.deltaTime;
                }
            }
        }
    }

    public void StartRound()
    {
        Dictionary<int, Vector3> selectedBlobIds = blobSelectorBar.GetSelectedBlobIds();
        blobSelector.SetActive(false);
        
        foreach(var selectedBlobId in selectedBlobIds)
        {
            Spawn(SaveSystem.saveData.blobData[selectedBlobId.Key], TeamTag.Player, selectedBlobId.Value);
        }  

        Destroy(spawnPointsParent);
        Time.timeScale = 1.0F;
        roundStarted = true;
    }

    private void Spawn(BlobStatsData blobStatsData, TeamTag teamTag, Vector3 blobPosition)
    {
        //CheckIfEnoughSpawnPoints(blobStatsData.Count, availableSpawnPoints.Count);

        GameObject blob = BlobInstantiator.GetBlobGameObject(blobStatsData, teamTag);

        blob.transform.localPosition = blobPosition;

        ObjectManager.GetInstance().AddObject(blob);
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
