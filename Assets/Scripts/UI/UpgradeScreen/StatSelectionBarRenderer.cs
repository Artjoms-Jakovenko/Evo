using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSelectionBarRenderer
{
    GameObject statBackground;
    GameObject parentBlock;
    GameObject statValueText;
    int currentPosition = 0;

    // Workaround to access dictionary by index, since dictionary element order is undefined
    List<KeyValuePair<StatName, Stat>> stats;

    public StatSelectionBarRenderer(GameObject parentBlock)
    {
        statBackground = Resources.Load("UI/EvolveShop/StatButton") as GameObject;
        statValueText = Resources.Load("UI/EvolveShop/StatValueText") as GameObject;
        this.parentBlock = parentBlock;
    }

    public void RenderStatSelectionUI(BlobStatsData blobStatsData,  int startPosition, Dictionary<StatName, StatUI> statDescriptions)
    {
        stats = blobStatsData.stats.ToList();

        RectTransform parentRectTransform = parentBlock.GetComponent<RectTransform>();
        RectTransform statBackgroundRectTransform = statBackground.GetComponent<RectTransform>();

        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(parentRectTransform.rect.width, 80.0F, statBackgroundRectTransform.rect.width, 20.0F);

        for (int i = 0; i < linearUiSpacing.partAmount; i++)
        {
            GameObject statBackgroundGameObject = InstantiateChild(statBackground, parentBlock);
            float relativeStart = -(parentRectTransform.rect.width - statBackgroundRectTransform.rect.width) / 2;
            statBackgroundGameObject.transform.localPosition = new Vector3(relativeStart + linearUiSpacing.GetNthPathPosition(i), 0.0F, 0.0F);

            // TODO add image
            // change image
            GameObject image = statBackgroundGameObject.transform.GetChild(0).gameObject; // TODO rework
            Image statImage = image.GetComponent<Image>();
            statImage.sprite = Resources.Load<Sprite>("UI/Stats/HealthIcon");
            // change image size

            // TODO change text
            TextMeshProUGUI statValueText = statBackground.GetComponentInChildren<TextMeshProUGUI>();
            statValueText.text = stats[i].Value.value.ToString();
        }


    }

    private GameObject InstantiateChild(GameObject asset, GameObject parent) // TODO move into helper class
    {
        GameObject gameObject = GameObject.Instantiate(asset);
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);

        return gameObject;
    }
}
