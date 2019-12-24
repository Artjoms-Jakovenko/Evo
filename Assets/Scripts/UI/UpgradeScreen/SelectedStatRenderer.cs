using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedStatRenderer
{
    GameObject upgradeLevelBackground;
    GameObject UpgradedLevel;
    GameObject NotUpgradedLevel;
    TextMeshProUGUI statTitleText;
    TextMeshProUGUI evolveValueText;
    TextMeshProUGUI maxValueText;

    public SelectedStatRenderer(GameObject upgradeLevelBackground, TextMeshProUGUI statTitleText, TextMeshProUGUI evolveValueText, TextMeshProUGUI maxValueText)
    {
        this.upgradeLevelBackground = upgradeLevelBackground;
        UpgradedLevel = Resources.Load("UI/EvolveShop/UpgradedLevel") as GameObject;
        NotUpgradedLevel = Resources.Load("UI/EvolveShop/NotUpgradedLevel") as GameObject;
        this.statTitleText = statTitleText;
        this.evolveValueText = evolveValueText;
        this.maxValueText = maxValueText;
    }
    public void UpdateSelectedStatUI(Stat stat) // TODO move creation to separate class
    {
        evolveValueText.text = stat.value + " <size=150%>→<size=100%> " + stat.GetNextLevelValue();
        maxValueText.text = "Max " + stat.maxValue.ToString();

        foreach (Transform child in upgradeLevelBackground.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        RectTransform rectTransform = upgradeLevelBackground.GetComponent<RectTransform>();

        float offset = 40.0F;
        float spacing = 20.0F;

        float barWidth = ((rectTransform.rect.width - offset * 2.0F) - stat.upgradeLevels * spacing) / stat.upgradeLevels;

        for (int i = 0; i < stat.upgradeLevels; i++)
        {
            GameObject gameObject = null;
            if (i < stat.currentUpgradeLevel)
            {
                gameObject = GameObject.Instantiate(UpgradedLevel);
            }
            else
            {
                gameObject = GameObject.Instantiate(NotUpgradedLevel);
            }

            gameObject.transform.SetParent(upgradeLevelBackground.transform);
            gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);

            RectTransform barRectTransform = gameObject.GetComponent<RectTransform>();
            barRectTransform.sizeDelta = new Vector2(barWidth, barRectTransform.rect.height);

            gameObject.transform.localPosition = new Vector3(offset - (rectTransform.rect.width - barRectTransform.rect.width) / 2 + (barWidth + spacing) * i, 22.0F - rectTransform.rect.height / 2, 0.0F);
        }
    }
}
