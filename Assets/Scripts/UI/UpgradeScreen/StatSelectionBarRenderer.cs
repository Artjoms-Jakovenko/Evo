﻿using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSelectionBarRenderer : SliderSelector
{
    public delegate void StatSelected();
    public static event StatSelected OnStatSelected;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject selectedStatWindow;

    GameObject statBackground;
    SelectedStatRenderer selectedStatRenderer;

    BlobStatsData lastBlobStatsData; // TODO rework
    StatName? selectedStat = null;

    // Workaround to access dictionary by index, since dictionary element order is undefined
    List<StatName> blobStatsDataKeys;

    public void Start()
    {
        statBackground = Resources.Load("UI/EvolveShop/StatButton") as GameObject;
        selectedStatRenderer = selectedStatWindow.GetComponent<SelectedStatRenderer>();

        RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
        RectTransform statBackgroundRectTransform = statBackground.GetComponent<RectTransform>();

        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(parentRectTransform.rect.width, 80.0F, statBackgroundRectTransform.rect.width, 20.0F);

        base.Initialize(leftArrow, rightArrow, linearUiSpacing);
    }

    public void RenderStatSelectionUI(BlobStatsData blobStatsData)
    {
        lastBlobStatsData = blobStatsData;
        SelectDefaultStatIfNothingIsSelected(); // TODO

        SetSelectedStat((StatName)selectedStat); // TODO look into default stat
        base.RenderSliderElements();
    }

    StatName? SelectDefaultStatIfNothingIsSelected()
    {
        // Sort keyValuePairs
        blobStatsDataKeys = new List<StatName>(lastBlobStatsData.stats.Keys);
        blobStatsDataKeys.Sort();

        // Assign default stat
        if (selectedStat == null) // TODO consider making cleaner
        {
            selectedStat = blobStatsDataKeys.First();
        }

        return selectedStat; // TODO
    }

    public StatName GetSelectedStatName()
    {
        return (StatName)selectedStat;
    }

    private void SetSelectedStat(StatName statName)
    {
        selectedStat = statName;
        selectedStatWindow.SetActive(true);
        selectedStatRenderer.UpdateSelectedStatUI(statName, lastBlobStatsData.stats[statName]);
    }

    public void StatButtonClicked(StatName statName)
    {
        SetSelectedStat(statName);
        OnStatSelected();
    }

    public override GameObject GetObjectAt(int position)
    {
        // Create background
        GameObject statBackgroundGameObject = GameObjectUtility.InstantiateChild(statBackground, gameObject, true);

        // Add events to buttons
        StatName statCaptured = blobStatsDataKeys[position]; // Important to keep this variable captured to avoid index issues
        statBackgroundGameObject.GetComponent<Button>().onClick.AddListener(() => StatButtonClicked(statCaptured));

        // Add image
        string imagePath = UiData.statDescriptions[blobStatsDataKeys[position]].statResourceImagePath;
        GameObject image = GameObjectUtility.InstantiateChild(Resources.Load<GameObject>(imagePath), statBackgroundGameObject, true);
        image.transform.localPosition = new Vector3(-8.0F, 15.0F, 0.0F);

        // Change text
        TextMeshProUGUI statValueText = statBackgroundGameObject.GetComponentInChildren<TextMeshProUGUI>();
        statValueText.text = lastBlobStatsData.stats[blobStatsDataKeys[position]].value.ToString();

        return statBackgroundGameObject;
    }

    public override int GetObjectCount()
    {
        return blobStatsDataKeys.Count;
    }
}
