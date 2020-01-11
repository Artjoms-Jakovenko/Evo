using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSelectionBarRenderer : MonoBehaviour // TODO This class should be generic split into specific and generic to be reused
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject selectedStatWindow;

    GameObject statBackground;
    GameObject statValueText;
    SelectedStatRenderer selectedStatRenderer;
    private StatName? selectedStat = null;

    int currentPosition = 0;
    List<GameObject> statButtons = new List<GameObject>();
    BlobStatsData lastBlobStatsData; // TODO rework

    // Workaround to access dictionary by index, since dictionary element order is undefined
    List<KeyValuePair<StatName, Stat>> stats;

    public void Awake()
    {
        statBackground = Resources.Load("UI/EvolveShop/StatButton") as GameObject;
        statValueText = Resources.Load("UI/EvolveShop/StatValueText") as GameObject;
        selectedStatRenderer = selectedStatWindow.GetComponent<SelectedStatRenderer>();
    }

    public void RenderStatSelectionUI(BlobStatsData blobStatsData)
    {
        lastBlobStatsData = blobStatsData;

        // Remove old buttons
        DeleteButtons();

        // Create buttons
        stats = lastBlobStatsData.stats.ToList();
        stats.Sort((x, y) => x.Key.CompareTo(y.Key));


        if(selectedStat == null) // TODO consider making cleaner
        {
            selectedStat = stats.First().Key;
        }
        SetSelectedStat((StatName)selectedStat);

        RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
        RectTransform statBackgroundRectTransform = statBackground.GetComponent<RectTransform>();

        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(parentRectTransform.rect.width, 80.0F, statBackgroundRectTransform.rect.width, 20.0F);

        // Show or hide left and right arrows
        if (currentPosition <= 0)
        {
            leftArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(true);
        }

        if (currentPosition + linearUiSpacing.partAmount >= stats.Count)
        {
            rightArrow.SetActive(false);
        }
        else
        {
            rightArrow.SetActive(true);
        }

        for (int i = 0; i < linearUiSpacing.partAmount; i++) // TODO split code in functions
        {
            // Create background
            GameObject statBackgroundGameObject = GameObjectUtility.InstantiateChild(statBackground, gameObject);
            float relativeStart = -(parentRectTransform.rect.width - statBackgroundRectTransform.rect.width) / 2;
            statBackgroundGameObject.transform.localPosition = new Vector3(relativeStart + linearUiSpacing.GetNthPathPosition(i), 0.0F, 0.0F);

            // Add events to buttons
            StatName statCaptured = stats[i + currentPosition].Key; // Important to keep this variable captured to avoid index issues
            statBackgroundGameObject.GetComponent<Button>().onClick.AddListener(() => SetSelectedStat(statCaptured));

            // Add image
            string imagePath = UiData.statDescriptions[stats[i + currentPosition].Key].statResourceImagePath;
            GameObject image = GameObjectUtility.InstantiateChild(Resources.Load<GameObject>(imagePath), statBackgroundGameObject);
            image.transform.localPosition = new Vector3(-8.0F, 15.0F, 0.0F);

            // Change text
            TextMeshProUGUI statValueText = statBackgroundGameObject.GetComponentInChildren<TextMeshProUGUI>();
            statValueText.text = stats[i + currentPosition].Value.value.ToString();

            statButtons.Add(statBackgroundGameObject);
        }
    }

    public void SetSelectedStat(StatName statName)
    {
        selectedStat = statName;
        selectedStatWindow.SetActive(true);
        selectedStatRenderer.UpdateSelectedStatUI(statName, lastBlobStatsData.stats[statName]);
    }

    public void ScrollOneRight()
    {
        currentPosition++;
        RenderStatSelectionUI(lastBlobStatsData);
    }

    public void ScrollOneLeft()
    {
        currentPosition--;
        RenderStatSelectionUI(lastBlobStatsData);
    }

    private void DeleteButtons()
    {
        foreach (GameObject gameObject in statButtons)
        {
            GameObject.Destroy(gameObject);
        }
        statButtons.Clear();
    }

    public StatName GetSelectedStat()
    {
        return (StatName)selectedStat;
    }
}
