using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatRenderer : MonoBehaviour
{
    private TextMeshProUGUI currentValueText;
    private TextMeshProUGUI upgradeValueText;
    private TextMeshProUGUI maxStatValueText;
    private TextMeshProUGUI statNameText;
    private Image statIcon;

    private EvolveButton evolveButton;

    private void Awake()
    {
        currentValueText = transform.Find("StatValue").GetComponent<TextMeshProUGUI>();
        upgradeValueText = transform.Find("StatValueIncrease").GetComponent<TextMeshProUGUI>();
        maxStatValueText = transform.Find("StatMaxValue").GetComponent<TextMeshProUGUI>();
        statNameText = transform.Find("StatName").GetComponent<TextMeshProUGUI>();

        statIcon = transform.Find("StatIcon").GetComponent<Image>();
        evolveButton = transform.Find("EvolveButton").GetComponent<EvolveButton>();
    }

    public void RenderStat(StatName statName, Stat stat)
    {
        currentValueText.text = stat.value.ToString();
        maxStatValueText.text = "Max " + stat.maxValue.ToString();

        statNameText.text = UiData.statDescriptions[statName].statDisplayName;
        statIcon.sprite = UiData.statDescriptions[statName].Icon;
        
        if (stat.IsMaxLevel())
        {
            upgradeValueText.text = "-";
            evolveButton.SetButtonValue(statName ,"Max", false, false);
        }
        else
        {
            string sign = "";
            float upgradeValue = stat.GetNextLevelValue() - stat.value;
            if (upgradeValue >= 0)
            {
                sign = "+ ";
            }
            else
            {
                sign = "- ";
            }
            upgradeValueText.text = sign + Math.Abs(upgradeValue).ToString();


            int upgradeCost = UpgradeCostCalculator.GetUpgradeCost(statName, stat);
            if(upgradeCost > SaveSystem.saveData.inventory.GetInventoryInfo(InventoryEnum.Money))
            {
                evolveButton.SetButtonValue(statName, upgradeCost.ToString(), false);
            }
            else
            {
                evolveButton.SetButtonValue(statName, upgradeCost.ToString());
            }
        }
    }
}
