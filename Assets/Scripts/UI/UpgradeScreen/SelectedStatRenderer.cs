using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedStatRenderer : MonoBehaviour
{
    public GameObject upgradeLevelBackground;
    public GameObject selectedStatBackground;
    public TextMeshProUGUI statTitleText;
    public TextMeshProUGUI evolveValueText;
    public TextMeshProUGUI maxValueText;
    GameObject upgradedLevel;
    GameObject notUpgradedLevel;

    void Awake()
    {
        upgradedLevel = Resources.Load("UI/EvolveShop/UpgradedLevel") as GameObject;
        notUpgradedLevel = Resources.Load("UI/EvolveShop/NotUpgradedLevel") as GameObject;
    }
    public void UpdateSelectedStatUI(StatName statName, Stat stat) // TODO move creation to separate class
    {
        evolveValueText.text = stat.value + " <size=150%>→<size=100%> " + stat.GetNextLevelValue();
        maxValueText.text = "Max " + stat.maxValue.ToString();
        statTitleText.text = UiData.statDescriptions[statName].statDisplayName;

        foreach (Transform child in upgradeLevelBackground.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in selectedStatBackground.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        string imagePath = UiData.statDescriptions[statName].statResourceImagePath;
        GameObject image = GameObjectUtility.InstantiateChild(Resources.Load<GameObject>(imagePath), selectedStatBackground, true);
        image.transform.localPosition = new Vector3(-8.0F, -25.0F, 0.0F);
        image.transform.localScale = new Vector3(1.25F, 1.25F, 1.25F);
        image.transform.SetAsFirstSibling(); // Render it behind info button

        RectTransform rectTransform = upgradeLevelBackground.GetComponent<RectTransform>();

        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(rectTransform.rect.width, 40.0F, 20.0F, stat.upgradeLevels);

        for (int i = 0; i < stat.upgradeLevels; i++)
        {
            GameObject gameObject = null;
            if (i < stat.currentUpgradeLevel)
            {
                gameObject = GameObjectUtility.InstantiateChild(upgradedLevel, upgradeLevelBackground, true);
            }
            else
            {
                gameObject = GameObjectUtility.InstantiateChild(notUpgradedLevel, upgradeLevelBackground, true);
            }

            RectTransform barRectTransform = gameObject.GetComponent<RectTransform>();
            barRectTransform.sizeDelta = new Vector2(linearUiSpacing.partLength, barRectTransform.rect.height);

            float barRelativeStart = -(rectTransform.rect.width - barRectTransform.rect.width) / 2;
            gameObject.transform.localPosition = new Vector3(barRelativeStart + linearUiSpacing.GetNthPathPosition(i), 22.0F - rectTransform.rect.height / 2, 0.0F);
        }
    }
}
