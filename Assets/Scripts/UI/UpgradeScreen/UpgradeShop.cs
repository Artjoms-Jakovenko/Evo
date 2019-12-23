using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UpgradeShop : MonoBehaviour // TODO make class to link stat name to its diplay name and description
{
    public GameObject shopCanvas;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI premiumMoneyText;
    public TextMeshProUGUI evolvePriceText;
    public TextMeshProUGUI currentStatValueHighlightText;
    public TextMeshProUGUI evolveValueText;
    public TextMeshProUGUI maxValueText;
    public TextMeshProUGUI statTitleText; // TODO

    public int blobID;
    private Stats selectedStat;

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
        
        int speedCost = UpgradeSystem.GetUpgradeCost(SaveDataUtility.GetBlobStats(saveData, blobID).Speed);
        selectedStat = SaveDataUtility.GetBlobStats(saveData, blobID).Speed.statName;

        Debug.Log(speedCost);
    }

    public void Upgrade()
    {
        SaveData saveData = SaveSystem.Load();
        int upgradeCost = UpgradeSystem.GetUpgradeCost(SaveDataUtility.GetBlobStats(saveData, blobID).Speed);
        if (CanUpgrade(SaveDataUtility.GetBlobStats(saveData, blobID).Speed, upgradeCost))
        {
            if (SaveDataUtility.GetBlobStats(saveData, blobID).Speed.Upgrade())
            {
                SaveDataUtility.PayMoney(saveData, upgradeCost);
            }
            else
            {
                Debug.Log("Can't upgrade. Max level already.");
            }

            SaveSystem.Save(saveData); // TODO double save, resets money
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

    private void UpdateUI()
    {
        SaveData saveData = SaveSystem.Load();

        moneyText.text = saveData.money.ToString();
        premiumMoneyText.text = saveData.premiumMoney.ToString();

        Stat stat = SaveDataUtility.GetBlobStats(saveData, blobID).Speed;
        int upgradeCost = UpgradeSystem.GetUpgradeCost(stat);
        evolvePriceText.text = "Evolve " + upgradeCost;
        currentStatValueHighlightText.text = stat.value.ToString();
        evolveValueText.text = stat.value + " <size=150%>→<size=100%> " + stat.GetNextLevelValue();
        maxValueText.text = "Max " + stat.maxValue.ToString();
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
}
