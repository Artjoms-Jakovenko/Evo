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
            statDescription = "How fast the blob moves. More speed will help the blob outrun other species.",
            statResourceImagePath = "UI/Stats/SpeedICon" // TODO load image object instead of path
        }
        },
        { StatName.Health, new StatUI()
        {
            statDisplayName = "Health",
            statDescription = "How much damage a blob can take before it dies. More health means the blob will likely survive longer.",
            statResourceImagePath = "UI/Stats/HealthIcon"
        }
        },
        { StatName.Sight, new StatUI()
        {
            statDisplayName = "Sight",
            statDescription = "How far a blob can notice things. More sight will help the blob make better decisions and react faster.",
            statResourceImagePath = "UI/Stats/HealthIcon" // TODO
        }
        },
        { StatName.ReactionTime, new StatUI()
        {
            statDisplayName = "Reaction time",
            statDescription = "How fast a blob can make decisions.", // TODO
            statResourceImagePath = "UI/Stats/HealthIcon" // TODO
        }
        },
        { StatName.MaxEnergy, new StatUI()
        {
            statDisplayName = "Max energy",
            statDescription = "How much energy can a blob store.", // TODO
            statResourceImagePath = "UI/Stats/HealthIcon" // TODO
        }
        },
    };

    public GameObject shopCanvas;
    public GameObject mainMenu;
    public GameObject statSelectionBar;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI premiumMoneyText;
    public TextMeshProUGUI evolvePriceText;

    SelectedStatRenderer selectedStatRenderer;

    public int blobID;
    private StatName selectedStat;

    private void Awake()
    {
        selectedStatRenderer = gameObject.GetComponentInChildren<SelectedStatRenderer>();
        SaveData saveData = SaveSystem.Load();
        selectedStatRenderer.UpdateSelectedStatUI(saveData.blobData[blobID].stats[StatName.Speed]); // TODO Remove

        StatSelectionBarRenderer statSelectionBarRenderer = gameObject.GetComponentInChildren<StatSelectionBarRenderer>();
        statSelectionBarRenderer.statDescriptions = statDescriptions; // TODO Remove
        statSelectionBarRenderer.RenderStatSelectionUI(saveData.blobData[blobID]); // TODO Remove
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
