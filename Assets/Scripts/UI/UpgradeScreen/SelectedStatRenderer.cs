using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedStatRenderer : MonoBehaviour
{
    public GameObject upgradeLevelBackground;
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
    public void UpdateSelectedStatUI(Stat stat) // TODO move creation to separate class
    {
        evolveValueText.text = stat.value + " <size=150%>→<size=100%> " + stat.GetNextLevelValue();
        maxValueText.text = "Max " + stat.maxValue.ToString();

        foreach (Transform child in upgradeLevelBackground.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        RectTransform rectTransform = upgradeLevelBackground.GetComponent<RectTransform>();

        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(rectTransform.rect.width, 40.0F, 20.0F, stat.upgradeLevels);

        for (int i = 0; i < stat.upgradeLevels; i++)
        {
            GameObject gameObject = null;
            if (i < stat.currentUpgradeLevel)
            {
                gameObject = GameObject.Instantiate(upgradedLevel); // TODO instantiate child to combine setparent and localscale mb
            }
            else
            {
                gameObject = GameObject.Instantiate(notUpgradedLevel);
            }

            gameObject.transform.SetParent(upgradeLevelBackground.transform);
            gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);

            RectTransform barRectTransform = gameObject.GetComponent<RectTransform>();
            barRectTransform.sizeDelta = new Vector2(linearUiSpacing.partLength, barRectTransform.rect.height);

            float barRelativeStart = -(rectTransform.rect.width - barRectTransform.rect.width) / 2;
            gameObject.transform.localPosition = new Vector3(barRelativeStart + linearUiSpacing.GetNthPathPosition(i), 22.0F - rectTransform.rect.height / 2, 0.0F);
        }
    }
}
