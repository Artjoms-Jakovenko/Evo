﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject mainMenu;
    public GameObject statSelectionBar;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI premiumMoneyText;
    public TextMeshProUGUI evolvePriceText;

    public int blobID;

    StatSelectionBarRenderer statSelectionBarRenderer;
    StatName lastSelectedStat;

    private void Awake()
    {
        SaveData saveData = SaveSystem.Load();

        statSelectionBarRenderer = gameObject.GetComponentInChildren<StatSelectionBarRenderer>();
        statSelectionBarRenderer.RenderStatSelectionUI(saveData.blobData[blobID]); // TODO Remove

        UpdateUI();
    }

    private void Update()
    {
        StatName selectedStat = statSelectionBarRenderer.GetSelectedStat();
        if(lastSelectedStat != selectedStat)
        {
            lastSelectedStat = selectedStat;
            UpdateUI();
        }
    }

    public void Upgrade()
    {
        SaveData saveData = SaveSystem.Load();
        int upgradeCost = UpgradeSystem.GetUpgradeCost(lastSelectedStat, saveData.blobData[blobID].stats[lastSelectedStat]);
        if (EnoughMoneyToUpgrade(saveData.blobData[blobID].stats[lastSelectedStat], upgradeCost))
        {
            if (saveData.blobData[blobID].stats[lastSelectedStat].Upgrade()) // Checks if stat is upgradeable
            {
                SaveDataUtility.PayMoney(saveData, upgradeCost);
            }
            else
            {
                Debug.Log("Can't upgrade. Max level already.");
            }

            SaveSystem.Save(saveData);
        }
        else
        {
            Debug.Log("Can't upgrade. Not enough money.");
        }

        UpdateUI();
    }

    bool EnoughMoneyToUpgrade(Stat stat, int upgradeCost)
    {
        if (upgradeCost <= SaveDataUtility.GetMoney()) 
        {
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        SaveData saveData = SaveSystem.Load();

        StatSelectionBarRenderer statSelectionBarRenderer = gameObject.GetComponentInChildren<StatSelectionBarRenderer>();
        statSelectionBarRenderer.RenderStatSelectionUI(saveData.blobData[blobID]); // TODO rewrite

        moneyText.text = saveData.money.ToString();
        premiumMoneyText.text = saveData.premiumMoney.ToString();

        StatName selectedStat = statSelectionBarRenderer.GetSelectedStat();

        Stat stat = saveData.blobData[blobID].stats[selectedStat];

        
        if (stat.IsMaxLevel())
        {
            evolvePriceText.text = "Max level";
        }
        else
        {
            int upgradeCost = UpgradeSystem.GetUpgradeCost(selectedStat, stat);
            evolvePriceText.text = "Evolve " + upgradeCost;
        }
    }

    public void EnableUI()
    {
        shopCanvas.SetActive(true);
        UpdateUI();
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
