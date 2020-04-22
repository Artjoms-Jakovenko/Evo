using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject mainMenu;
    public GameObject statSelectionBar;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI premiumMoneyText;
    public TextMeshProUGUI evolvePriceText;
    public Button evolveButton;
    public BlobSelectScreen blobSelectScreen;

    public GameObject statBlockParent;

    public StatRenderer test;
    private List<GameObject> statBlocks;

    private int selectedBlobId;

    private void OnEnable()
    {
        UpdateMoney();

        BlobSelectScreen.OnBlobSelected += SelectBlob;
        EvolveButton.OnEvolveButtonClicked += UpgradeButtonClicked;
    }

    private void OnDisable()
    {
        BlobSelectScreen.OnBlobSelected -= SelectBlob;
        EvolveButton.OnEvolveButtonClicked -= UpgradeButtonClicked;
    }

    private void Awake()
    {
        statBlocks = new List<GameObject>();
        foreach (Transform transform in statBlockParent.transform)
        {
            statBlocks.Add(transform.gameObject);
        }
    }

    private void Start()
    {
        selectedBlobId = SaveSystem.saveData.lastSelectedBlobInUpgradeShop;
    }

    public void Upgrade(StatName statName)
    {
        int upgradeCost = GetUpgradeCost(statName);
        if (EnoughMoneyToUpgrade(upgradeCost))
        {
            if (SaveSystem.saveData.blobData[selectedBlobId].stats[statName].Upgrade()) // Checks if stat is upgradeable
            {
                SaveDataUtility.PayMoney(upgradeCost);
            }
            else
            {
                Debug.Log("Can't upgrade. Max level already.");
            }

            SaveSystem.Save();
        }
        else
        {
            Debug.Log("Can't upgrade. Not enough money.");
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

    public void SelectBlobButton()
    {
        blobSelectScreen.SelectBlob();
    }

    private int GetUpgradeCost(StatName selectedStat)
    {
        return UpgradeCostCalculator.GetUpgradeCost(selectedStat, SaveSystem.saveData.blobData[selectedBlobId].stats[selectedStat]);
    }

    private bool EnoughMoneyToUpgrade(int upgradeCost)
    {
        if (upgradeCost <= SaveSystem.saveData.inventory.GetInventoryInfo(InventoryEnum.Money)) 
        {
            return true;
        }
        return false;
    }

    private void UpdateMoney()
    {
        moneyText.text = SaveSystem.saveData.inventory.GetInventoryInfo(InventoryEnum.Money).ToString();
        premiumMoneyText.text = SaveSystem.saveData.inventory.GetInventoryInfo(InventoryEnum.PremiumMoney).ToString();
    }

    private void UpdateUI() // TODO
    {
        UpdateMoney();
        RenderStats(selectedBlobId);
    }

    private void SelectBlob(int blobId)
    {
        selectedBlobId = blobId;

        SaveSystem.saveData.lastSelectedBlobInUpgradeShop = blobId;
        SaveSystem.Save();

        UpdateUI();
    }

    private void RenderStats(int blobId)
    {
        foreach (var statBlock in statBlocks)
        {
            statBlock.SetActive(false);
        }

        List<StatName> blobStatNames = new List<StatName>(SaveSystem.saveData.blobData[blobId].stats.Keys); // TODO check performance
        blobStatNames.Sort();

        for (int i = 0; i < SaveSystem.saveData.blobData[blobId].stats.Count; i++)
        {
            statBlocks[i].GetComponent<StatRenderer>().RenderStat(blobStatNames[i], SaveSystem.saveData.blobData[blobId].stats[blobStatNames[i]]);
            statBlocks[i].SetActive(true);
        }
    }

    private void UpgradeButtonClicked(StatName statName)
    {
        Upgrade(statName);

        UpdateUI();
    }
}
