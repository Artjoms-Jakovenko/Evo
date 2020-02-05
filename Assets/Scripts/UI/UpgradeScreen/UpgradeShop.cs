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

    private int selectedBlobId = 0; // TODO last opened blob

    private void OnEnable()
    {
        SaveData saveData = SaveSystem.Load();
        UpdateMoney(saveData);

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
    }

    private void SelectedStatChanged()
    {
        selectedStat = statSelectionBarRenderer.GetSelectedStatName();
        UpdateUI();
    }

    public void Upgrade()
    {
        SaveData saveData = SaveSystem.Load();
        int upgradeCost = GetUpgradeCost(saveData);
        if (EnoughMoneyToUpgrade(saveData, upgradeCost))
        {
            if (saveData.blobData[selectedBlobId].stats[selectedStat].Upgrade()) // Checks if stat is upgradeable
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

    private int GetUpgradeCost(SaveData saveData)
    {
        return UpgradeSystem.GetUpgradeCost(selectedStat, saveData.blobData[selectedBlobId].stats[selectedStat]);
    }

    bool EnoughMoneyToUpgrade(SaveData saveData, int upgradeCost)
    {
        if (upgradeCost <= saveData.money) 
        {
            return true;
        }
        return false;
    }

    private void UpdateMoney(SaveData saveData)
    {
        moneyText.text = saveData.money.ToString();
        premiumMoneyText.text = saveData.premiumMoney.ToString();
    }

    private void UpdateUI()
    {
        SaveData saveData = SaveSystem.Load();
        UpdateMoney(saveData);

        Stat stat = saveData.blobData[selectedBlobId].stats[selectedStat];
        UpdateEvolveButton(saveData);

        statSelectionBarRenderer.RenderStatSelectionUI(saveData.blobData[selectedBlobId]);
    }

    private void UpdateEvolveButton(SaveData saveData)
    {
        Stat stat = saveData.blobData[selectedBlobId].stats[selectedStat];

        if (stat.IsMaxLevel())
        {
            evolvePriceText.text = "Max level";
            evolveButton.interactable = false;
        }
        else
        {
            int upgradeCost = UpgradeSystem.GetUpgradeCost(selectedStat, stat);

            if (EnoughMoneyToUpgrade(saveData, upgradeCost))
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

    public void SelectBlob(int blobId)
    {
        selectedBlobId = blobId;

        SaveData saveData = SaveSystem.Load();

        statSelectionBarRenderer.RenderStatSelectionUI(saveData.blobData[selectedBlobId]); // TODO Remove

        UpdateUI();
    }
}
