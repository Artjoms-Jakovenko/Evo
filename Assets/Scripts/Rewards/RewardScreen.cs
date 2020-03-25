using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardScreen : MonoBehaviour
{
    public GameObject nextLevelButton;
    public GameObject rewardContent;

    private GameObject rewardBlockAsset;
    private Dictionary<InventoryEnum, int> rewards;

    LevelEnum nextLevel;
    private void OnEnable()
    {
        nextLevel = LevelManager.GetLevelEnum(SceneManager.GetActiveScene().name);
        if (!LevelManager.IsNextLevelUnlocked(nextLevel))
        {
            nextLevelButton.SetActive(false);
        }
    }

    private void Awake()
    {
        rewardBlockAsset = Resources.Load<GameObject>("UI/RewardBlock");
    }

    public void AdministerRewards(LevelEnum levelName, int starsAchieved)
    {
        rewards = LevelInfoData.GetLevelRewards(levelName, LevelManager.GetLevelProgress(levelName).starCount, starsAchieved);

        foreach (var reward in rewards)
        {
            SaveSystem.saveData.inventory.AddToInventory(reward.Key, reward.Value);
        }

        SaveSystem.Save();
    }

    public void RenderRewards()
    {
        foreach (var reward in rewards)
        {
            GameObject rewardBlock = GameObjectUtility.InstantiateChild(rewardBlockAsset, rewardContent, true);

            rewardBlock.transform.Find("RewardAmountText").GetComponent<TextMeshProUGUI>().text = reward.Value.ToString();

            Sprite rewardIcon = Resources.Load<Sprite>(UiData.inventoryDescription[reward.Key].iconPath);
            rewardBlock.transform.Find("RewardIcon").GetComponent<Image>().sprite = rewardIcon;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        LevelManager.Load(nextLevel, true);
    }
}
