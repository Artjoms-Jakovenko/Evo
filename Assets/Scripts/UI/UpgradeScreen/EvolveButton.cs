﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvolveButton : MonoBehaviour
{
    private Button evolveButton;
    private TextMeshProUGUI buttonValue;

    public delegate void EvolveButtonClicked(StatName statName);
    public static event EvolveButtonClicked OnEvolveButtonClicked;

    private StatName associatedStat;

    private void Awake()
    {
        evolveButton = GetComponent<Button>();
        buttonValue = transform.Find("EvolvePriceText").GetComponent<TextMeshProUGUI>();
    }

    public void SetButtonValue(StatName associatedStat, string value, bool interactable = true, bool showIcon = true) // Sprite 0 is primary money icon after the text
    {
        this.associatedStat = associatedStat;

        if (showIcon)
        {
            buttonValue.text = value + " <sprite=0>";
        }
        else
        {
            buttonValue.text = value;
        }

        evolveButton.interactable = interactable;
    }

    public void ButtonClick()
    {
        OnEvolveButtonClicked(associatedStat);
    }
}
