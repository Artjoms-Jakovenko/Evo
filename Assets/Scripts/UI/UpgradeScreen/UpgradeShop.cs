using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    private Dictionary<StatName, StatUI> statDescriptions = new Dictionary<StatName, StatUI>()
    {
        { StatName.Speed, new StatUI()
        {
            statDisplayName = "Speed",
            statDescription = "How fast the blob moves",
            statResourceImagePath = "UI/Stats/Speed" // TODO
        }
        },
        { StatName.Health, new StatUI()
        {
            statDisplayName = "Health",
            statDescription = "How much damage a blob can take before it dies.",
            statResourceImagePath = "UI/Stats/Health" // TODO
        }
        },
        { StatName.Sight, new StatUI()
        {
            statDisplayName = "Sight",
            statDescription = "How far a blob can notice things.",
            statResourceImagePath = "UI/Stats/Sight" // TODO
        }
        },
    };

    public GameObject shopCanvas;
    public GameObject mainMenu;
    public GameObject upgradeLevelBackground;
    public GameObject statSelectionBar;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI premiumMoneyText;
    public TextMeshProUGUI evolvePriceText;
    public TextMeshProUGUI currentStatValueHighlightText;
    public TextMeshProUGUI evolveValueText;
    public TextMeshProUGUI maxValueText;
    public TextMeshProUGUI statTitleText; // TODO

    private SelectedStatRenderer selectedStatRenderer;

    public int blobID;
    private StatName selectedStat;

    private void Awake()
    {
        selectedStatRenderer = new SelectedStatRenderer(upgradeLevelBackground, statTitleText, evolveValueText, maxValueText);
        SaveData saveData = SaveSystem.Load();
        selectedStatRenderer.UpdateSelectedStatUI(saveData.blobData[blobID].stats[StatName.Speed]); // TODO Remove

        StatSelectionBarRenderer statSelectionBarRenderer = new StatSelectionBarRenderer(statSelectionBar);
        statSelectionBarRenderer.RenderStatSelectionUI(saveData.blobData[blobID], 0, statDescriptions); // TODO Remove
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
        
        int speedCost = UpgradeSystem.GetUpgradeCost(StatName.Speed, saveData.blobData[blobID].stats[StatName.Speed]);
        selectedStat = StatName.Speed; // TODO fix this mess

        Debug.Log(speedCost);
    }

    public void Upgrade()
    {
        SaveData saveData = SaveSystem.Load();
        int upgradeCost = UpgradeSystem.GetUpgradeCost(StatName.Speed, saveData.blobData[blobID].stats[StatName.Speed]);
        if (CanUpgrade(saveData.blobData[blobID].stats[StatName.Speed], upgradeCost))
        {
            if (saveData.blobData[blobID].stats[StatName.Speed].Upgrade())
            {
                SaveDataUtility.PayMoney(saveData, upgradeCost);
            }
            else
            {
                Debug.Log("Can't upgrade. Max level already.");
            }

            SaveSystem.Save(saveData); // TODO double save, resets money

            selectedStatRenderer.UpdateSelectedStatUI(saveData.blobData[blobID].stats[StatName.Speed]);
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

        Stat stat = saveData.blobData[blobID].stats[StatName.Speed]; // TODO unhardcode this

        currentStatValueHighlightText.text = stat.value.ToString();

        int upgradeCost = UpgradeSystem.GetUpgradeCost(StatName.Speed, stat); // TODO unhardcode this
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
