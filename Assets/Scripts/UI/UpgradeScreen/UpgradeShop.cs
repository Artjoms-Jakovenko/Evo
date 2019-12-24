using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    private Dictionary<Stats, StatUI> statDescriptions = new Dictionary<Stats, StatUI>()
    {
        { Stats.Speed, new StatUI()
        {
            statDisplayName = "Speed",
            statDescription = "How fast the blob moves",
            statResourceImagePath = null // TODO
        }
        },
        { Stats.Health, new StatUI()
        {
            statDisplayName = "Health",
            statDescription = "How much damage a blob can take before it dies.",
            statResourceImagePath = null // TODO
        }
        },
        { Stats.Sight, new StatUI()
        {
            statDisplayName = "Sight",
            statDescription = "How far a blob can notice things.",
            statResourceImagePath = null // TODO
        }
        },
    };

    public GameObject shopCanvas;
    public GameObject mainMenu;
    public GameObject upgradeLevelBackground;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI premiumMoneyText;
    public TextMeshProUGUI evolvePriceText;
    public TextMeshProUGUI currentStatValueHighlightText;
    public TextMeshProUGUI evolveValueText;
    public TextMeshProUGUI maxValueText;
    public TextMeshProUGUI statTitleText; // TODO

    private SelectedStatRenderer selectedStatRenderer;

    public int blobID;
    private Stats selectedStat;

    private void Awake()
    {
        selectedStatRenderer = new SelectedStatRenderer(upgradeLevelBackground, statTitleText, evolveValueText, maxValueText);
        SaveData saveData = SaveSystem.Load();
        selectedStatRenderer.UpdateSelectedStatUI(saveData.blobData[blobID].Speed); // TODO Remove
    }

    private void Update()
    {
        if (shopCanvas.activeSelf)
        {
            UpdateUI();
        }
    }

    void PrintUpgradePrices()
    {
        SaveData saveData = SaveSystem.Load();
        
        int speedCost = UpgradeSystem.GetUpgradeCost(saveData.blobData[blobID].Speed);
        selectedStat = saveData.blobData[blobID].Speed.statName;

        Debug.Log(speedCost);
    }

    public void Upgrade()
    {
        SaveData saveData = SaveSystem.Load();
        int upgradeCost = UpgradeSystem.GetUpgradeCost(saveData.blobData[blobID].Speed);
        if (CanUpgrade(saveData.blobData[blobID].Speed, upgradeCost))
        {
            if (saveData.blobData[blobID].Speed.Upgrade())
            {
                SaveDataUtility.PayMoney(saveData, upgradeCost);
            }
            else
            {
                Debug.Log("Can't upgrade. Max level already.");
            }

            SaveSystem.Save(saveData); // TODO double save, resets money

            selectedStatRenderer.UpdateSelectedStatUI(saveData.blobData[blobID].Speed);
        }
        else
        {
            Debug.Log("Can't upgrade. Not enough money.");
        }
    }

    bool CanUpgrade(Stat stat, int upgradeCost)
    {
        if (upgradeCost <= SaveDataUtility.GetMoney()) 
        {
            if (stat.CanUpgrade())
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateUI() // TODO move stat on evolve 
    {
        SaveData saveData = SaveSystem.Load();

        moneyText.text = saveData.money.ToString();
        premiumMoneyText.text = saveData.premiumMoney.ToString();

        Stat stat = saveData.blobData[blobID].Speed;

        currentStatValueHighlightText.text = stat.value.ToString();

        int upgradeCost = UpgradeSystem.GetUpgradeCost(stat);
        evolvePriceText.text = "Evolve " + upgradeCost;
    }

    public void EnableUI()
    {
        UpdateUI();
        shopCanvas.SetActive(true);
    }

    public void DisableUI()
    {
        shopCanvas.SetActive(false);
    }

    public void GoBackToMenu()
    {
        DisableUI();
        mainMenu.GetComponent<MainMenuUI>().EnableUI();
    }
}
