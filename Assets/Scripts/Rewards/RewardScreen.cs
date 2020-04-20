using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardScreen : MonoBehaviour
{
    public Button nextLevelButton;
    public GameObject rewardContent;
    public Material asbestosTextMaterial;

    private GameObject rewardBlockAsset;

    private Dictionary<InventoryEnum, int> rewards;
    private int starsAchieved = 0;
    bool completed = false;

    private Image leftStar;
    private Image centerStar;
    private Image rightStar;
    private Sprite orangeStar;

    LevelEnum nextLevel;
    private void OnEnable()
    {
        nextLevel = LevelManager.GetLevelEnum(SceneManager.GetActiveScene().name);
        if (!LevelManager.IsNextLevelUnlocked(nextLevel))
        {
            nextLevelButton.interactable = false;
        }
    }

    private void Awake()
    {
        rewardBlockAsset = Resources.Load<GameObject>("UI/Rewards/RewardBlock");
        leftStar = transform.Find("RewardScreenCanvas/Background/LeftStar").GetComponent<Image>();
        centerStar = transform.Find("RewardScreenCanvas/Background/CenterStar").GetComponent<Image>();
        rightStar = transform.Find("RewardScreenCanvas/Background/RightStar").GetComponent<Image>();
        orangeStar = Resources.Load<Sprite>("UI/LevelGoals/StarIcon");
    }

    public void AdministerRewards(LevelEnum levelName, bool completed, int starsAchieved)
    {
        rewards = LevelInfoData.GetLevelRewards(levelName, completed, LevelManager.GetLevelProgress(levelName).starCount, starsAchieved);
        this.starsAchieved = starsAchieved;
        this.completed = completed;

        foreach (var reward in rewards)
        {
            SaveSystem.saveData.inventory.AddToInventory(reward.Key, reward.Value);
        }

        SaveSystem.Save();
    }

    public void RenderRewardScreen()
    {
        SetStarColor();

        if (!completed)
        {
            Sprite grayscaleBackground = Resources.Load<Sprite>("UI/RewardScreen/VictoryScreenBackgroundGrayscale");
            transform.Find("RewardScreenCanvas/Background").GetComponent<Image>().sprite = grayscaleBackground;

            TextMeshProUGUI victoryText = transform.Find("RewardScreenCanvas/VictoryText").GetComponent<TextMeshProUGUI>();
            victoryText.text = "Defeat...";
            victoryText.fontSharedMaterial = asbestosTextMaterial;

            TextMeshProUGUI rewardsText = transform.Find("RewardScreenCanvas/RewardsText").GetComponent<TextMeshProUGUI>();
            rewardsText.fontSharedMaterial = asbestosTextMaterial;
        }

        foreach (var reward in rewards)
        {
            GameObject rewardBlock = GameObjectUtility.InstantiateChild(rewardBlockAsset, rewardContent, true);

            TextMeshProUGUI rewardAmountText = rewardBlock.transform.Find("RewardAmountText").GetComponent<TextMeshProUGUI>();
            rewardAmountText.text = reward.Value.ToString();
            if (!completed)
            {
                rewardAmountText.fontSharedMaterial = asbestosTextMaterial;
            }

            Sprite rewardIcon = UiData.inventoryDescription[reward.Key].Icon;
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

    private void SetStarColor()
    {
        if(starsAchieved >= 1)
        {
            leftStar.sprite = orangeStar;
            leftStar.color = Color.white;
        }

        if(starsAchieved >= 2)
        {
            centerStar.sprite = orangeStar;
            centerStar.color = Color.white;
        }

        if(starsAchieved >= 3)
        {
            rightStar.sprite = orangeStar;
            rightStar.color = Color.white;
        }
    }
}
