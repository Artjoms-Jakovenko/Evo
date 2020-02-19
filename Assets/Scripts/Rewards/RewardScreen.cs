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

    string nextLevelName;
    private void Awake()
    {
        nextLevelName = LevelManager.GetNextLevelName(SceneManager.GetActiveScene().name);
        if(nextLevelName == null)
        {
            nextLevelButton.SetActive(false);
        }

        rewardBlockAsset = Resources.Load<GameObject>("UI/RewardBlock");
    }

    public void AdministerRewards(LevelEnum levelName, int starsAchieved) // TODO
    {
        SaveData saveData = SaveSystem.Load();

        Dictionary<InventoryEnum, int> rewards = LevelInfoData.GetLevelRewards(levelName, 0, starsAchieved); // TODO read stars from savefile

        foreach (var reward in rewards)
        {
            saveData.inventory.AddToInventory(reward.Key, reward.Value);
        }

        SaveSystem.Save(saveData);

        RenderRewards(rewards);
    }

    public void RenderRewards(Dictionary<InventoryEnum, int> rewards)
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
        SceneManager.LoadScene(nextLevelName);
    }
}
