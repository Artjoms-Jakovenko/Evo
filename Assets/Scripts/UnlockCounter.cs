using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockCounter : MonoBehaviour
{
    public ObjectTag tage;
    public int count;

    bool unlocked = false;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ObjectManager.GetInstance().GetAllWithTags(new List<ObjectTag>() { tage }).Count + "/" + count;
    }
}
