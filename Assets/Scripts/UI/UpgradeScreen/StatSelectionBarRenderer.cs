using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSelectionBarRenderer : SliderSelector
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject selectedStatWindow;

    GameObject statBackground;
    SelectedStatRenderer selectedStatRenderer;
    private StatName? selectedStat = null;

    BlobStatsData lastBlobStatsData; // TODO rework

    // Workaround to access dictionary by index, since dictionary element order is undefined
    List<KeyValuePair<StatName, Stat>> stats;

    public void Awake()
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

        // Sort keyValuePairs
        stats = lastBlobStatsData.stats.ToList();
        stats.Sort((x, y) => x.Key.CompareTo(y.Key));

        // Assign default stat
        if(selectedStat == null) // TODO consider making cleaner
        {
            selectedStat = stats.First().Key;
        }
        SetSelectedStat((StatName)selectedStat);
        base.RenderSliderElements();
    }

    public void SetSelectedStat(StatName statName)
    {
        selectedStat = statName;
        selectedStatWindow.SetActive(true);
        selectedStatRenderer.UpdateSelectedStatUI(statName, lastBlobStatsData.stats[statName]);
    }

    public StatName GetSelectedStat()
    {
        return (StatName)selectedStat;
    }

    public override GameObject GetObjectAt(int position)
    {
        // Create background
        GameObject statBackgroundGameObject = GameObjectUtility.InstantiateChild(statBackground, gameObject, true);

        // Add events to buttons
        StatName statCaptured = stats[position].Key; // Important to keep this variable captured to avoid index issues
        statBackgroundGameObject.GetComponent<Button>().onClick.AddListener(() => SetSelectedStat(statCaptured));

        // Add image
        string imagePath = UiData.statDescriptions[stats[position].Key].statResourceImagePath;
        GameObject image = GameObjectUtility.InstantiateChild(Resources.Load<GameObject>(imagePath), statBackgroundGameObject, true);
        image.transform.localPosition = new Vector3(-8.0F, 15.0F, 0.0F);

        // Change text
        TextMeshProUGUI statValueText = statBackgroundGameObject.GetComponentInChildren<TextMeshProUGUI>();
        statValueText.text = stats[position].Value.value.ToString();

        return statBackgroundGameObject;
    }

    public override int GetObjectCount()
    {
        return stats.Count;
    }
}
