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

    StatSelectionBarRenderer statSelectionBarRenderer;
    StatName selectedStat;

    private int selectedBlobId;

    private void OnEnable()
    {
        UpdateMoney();

        StatSelectionBarRenderer.OnStatSelected += SelectedStatChanged;
        BlobSelectScreen.OnBlobSelected += SelectBlob;
    }

    private void OnDisable()
    {
        StatSelectionBarRenderer.OnStatSelected -= SelectedStatChanged;
        BlobSelectScreen.OnBlobSelected -= SelectBlob;
    }

    private void Start()
    {
        statSelectionBarRenderer = gameObject.GetComponentInChildren<StatSelectionBarRenderer>();

        selectedBlobId = SaveSystem.saveData.lastSelectedBlobInUpgradeShop;
        UpdateBlobUI();
    }

    public void Upgrade()
    {
        int upgradeCost = GetUpgradeCost();
        if (EnoughMoneyToUpgrade(upgradeCost))
        {
            if (SaveSystem.saveData.blobData[selectedBlobId].stats[selectedStat].Upgrade()) // Checks if stat is upgradeable
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

        UpdateUI();
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

    private void SelectedStatChanged()
    {
        selectedStat = statSelectionBarRenderer.GetSelectedStatName();
        UpdateUI();
    }

    private int GetUpgradeCost()
    {
        return UpgradeSystem.GetUpgradeCost(selectedStat, SaveSystem.saveData.blobData[selectedBlobId].stats[selectedStat]);
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

    private void UpdateUI()
    {
        UpdateMoney();
        UpdateEvolveButton();
        statSelectionBarRenderer.RenderStatSelectionUI(SaveSystem.saveData.blobData[selectedBlobId]);
    }

    private void UpdateEvolveButton()
    {
        Stat stat = SaveSystem.saveData.blobData[selectedBlobId].stats[selectedStat];

        if (stat.IsMaxLevel())
        {
            evolvePriceText.text = "Max level";
            evolveButton.interactable = false;
        }
        else
        {
            int upgradeCost = UpgradeSystem.GetUpgradeCost(selectedStat, stat);

            if (EnoughMoneyToUpgrade(upgradeCost))
            {
                evolvePriceText.text = "Evolve " + upgradeCost;
                evolveButton.interactable = true;
            }
            else
            {
                evolvePriceText.text = "Not enough evo";
                evolveButton.interactable = false;
            }
        }
    }

    private void SelectBlob(int blobId)
    {
        selectedBlobId = blobId;

        UpdateBlobUI();
        SaveSystem.saveData.lastSelectedBlobInUpgradeShop = blobId;
        SaveSystem.Save();
    }

    private void UpdateBlobUI()
    {
        statSelectionBarRenderer.RenderStatSelectionUI(SaveSystem.saveData.blobData[selectedBlobId]);
        selectedStat = statSelectionBarRenderer.GetSelectedStatName();
        UpdateUI();
    }
}
