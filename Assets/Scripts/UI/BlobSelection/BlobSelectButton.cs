using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlobSelectButton : MonoBehaviour
{
    public GameObject sliderContent;
    public Image blobIcon;
    public TextMeshProUGUI blobName;

    private GameObject statBlock;

    private void Awake()
    {
        statBlock = Resources.Load<GameObject>("UI/StatBlock");
    }

    public void RenderButton(BlobStatsData blobStats)
    {
        // Set blob type icon
        Sprite blobIconSprite = UiData.blobTypeDescription[blobStats.blobType].Icon;
        blobIcon.sprite = blobIconSprite;

        // Set blob name
        blobName.text = blobStats.blobName;

        // Add stat info
        foreach (var stat in blobStats.stats)
        {
            GameObject statBlockObject = GameObjectUtility.InstantiateChild(statBlock, sliderContent, true);

            Image statIcon = statBlockObject.transform.Find("StatIcon").GetComponent<Image>();
            statIcon.sprite = UiData.statDescriptions[stat.Key].Icon;
            
            TextMeshProUGUI statValueText = statBlockObject.transform.Find("StatValueText").GetComponent<TextMeshProUGUI>();
            statValueText.text = stat.Value.value.ToString();
        }
    }
}
