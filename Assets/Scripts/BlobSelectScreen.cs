using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlobSelectScreen : MonoBehaviour
{
    public GameObject blobSelectScreen;

    private IBlobSelectObserver blobSelectObserver;

    void Start()
    {
        SaveData saveData = SaveSystem.Load();

        float containerHeight = saveData.blobData.Count * 300.0F;
        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(containerHeight, 0.0F, 200.0F, saveData.blobData.Count);

        GameObject blobBarAsset = Resources.Load("TempButton") as GameObject; // TODO

        int i = 0; // TODO split dictionary in keyvaluepairs
        foreach (var blob in saveData.blobData)
        {
            GameObject blobBar = GameObjectUtility.InstantiateChild(blobBarAsset, gameObject, true);
            blobBar.GetComponentInChildren<TextMeshProUGUI>().text = blob.Key.ToString();

            RectTransform blobBarRectTransform = blobBar.GetComponent<RectTransform>();
            blobBarRectTransform.transform.localPosition = new Vector3(0.0F, linearUiSpacing.GetNthPathPosition(i));
            i++; // TODO fix loops
        }
    }

    void MarkBlobsAsSelected(List<int> blobIds) // TODO 
    {

    }

    public void SelectBlob(IBlobSelectObserver blobSelectObserver)
    {
        blobSelectScreen.SetActive(true);
        this.blobSelectObserver = blobSelectObserver;
    }
}
