using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBackgroundScaler : MonoBehaviour
{
    public Image background;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Rescale() // TODO test
    {
        background.rectTransform.sizeDelta = new Vector2(text.preferredWidth + 24F, text.preferredHeight + 4F);
    }
}
