using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSelectionBarRenderer : MonoBehaviour
{
    public delegate void StatSelected();
    public static event StatSelected OnStatSelected;

    public GameObject selectedStatWindow;
    public GameObject sliderContent;

    GameObject statBackground;
    SelectedStatRenderer selectedStatRenderer;

    BlobStatsData selectedBlobStatsData;
    StatName? selectedStat = null;

    // Workaround to access dictionary by index, since dictionary element order is undefined
    List<StatName> blobStatsDataKeys;

    public void Awake()
    {
        statBackground = Resources.Load("UI/EvolveShop/StatButton") as GameObject;
        selectedStatRenderer = selectedStatWindow.GetComponent<SelectedStatRenderer>();
    }

    public void RenderStatSelectionUI(BlobStatsData blobStatsData)
    {
        selectedBlobStatsData = blobStatsData;
        SelectDefaultStatIfNothingIsSelected();
        SetSelectedStat((StatName)selectedStat);

        DeleteOldButtons();
        for(int i = 0; i < blobStatsDataKeys.Count; i++)
        {
            CreateObjectAt(i);
        }
    }    

    public StatName GetSelectedStatName()
    {
        return (StatName)selectedStat;
    }


    public void StatButtonClicked(StatName statName)
    {
        SetSelectedStat(statName);
        OnStatSelected();
    }

    private GameObject CreateObjectAt(int position)
    {
        // Create background
        GameObject statBackgroundGameObject = GameObjectUtility.InstantiateChild(statBackground, sliderContent, true);

        // Add events to buttons
        StatName statCaptured = blobStatsDataKeys[position]; // Important to keep this variable captured to avoid index issues
        statBackgroundGameObject.GetComponent<Button>().onClick.AddListener(() => StatButtonClicked(statCaptured));

        // Add image
        string imagePath = UiData.statDescriptions[blobStatsDataKeys[position]].statResourceImagePath;
        GameObject image = GameObjectUtility.InstantiateChild(Resources.Load<GameObject>(imagePath), statBackgroundGameObject, true);
        image.transform.localPosition = new Vector3(-8.0F, 15.0F, 0.0F);

        // Change text
        TextMeshProUGUI statValueText = statBackgroundGameObject.GetComponentInChildren<TextMeshProUGUI>();
        statValueText.text = selectedBlobStatsData.stats[blobStatsDataKeys[position]].value.ToString();

        return statBackgroundGameObject;
    }

    private void DeleteOldButtons()
    {
        foreach (Transform child in sliderContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SetSelectedStat(StatName statName)
    {
        selectedStat = statName;
        selectedStatWindow.SetActive(true);
        selectedStatRenderer.UpdateSelectedStatUI(statName, selectedBlobStatsData.stats[statName]);
    }

    private void SelectDefaultStatIfNothingIsSelected()
    {
        // Sort keyValuePairs
        blobStatsDataKeys = new List<StatName>(selectedBlobStatsData.stats.Keys);
        blobStatsDataKeys.Sort();

        // Assign default stat
        if (selectedStat == null)
        {
            selectedStat = blobStatsDataKeys.First();
        }
        else if (!blobStatsDataKeys.Contains((StatName)selectedStat))
        {
            selectedStat = blobStatsDataKeys.First();
        }
    }
}
