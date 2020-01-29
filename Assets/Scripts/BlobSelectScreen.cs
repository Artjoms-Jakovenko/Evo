﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlobSelectScreen : MonoBehaviour
{
    public GameObject blobSelectScreen;

    private IBlobSelectObserver blobSelectObserver;
    private List<int> alreadySelectedBlobs = new List<int>();

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

            // Add events to buttons
            blobBar.GetComponent<Button>().onClick.AddListener(() => BlobClicked(blob.Key));

            i++; // TODO fix loops
        }
    }

    void BlobClicked(int blobId)
    {
        blobSelectObserver.SelectedBlob(blobId);
        blobSelectScreen.SetActive(false);
    }

    public void SelectBlob(IBlobSelectObserver blobSelectObserver)
    {
        blobSelectScreen.SetActive(true);
        this.blobSelectObserver = blobSelectObserver;
        //alreadySelectedBlobs = new List<int>();
    }

    /*public void SelectBlob(IBlobSelectObserver blobSelectObserver, List<int> selectedBlobIds)
    {
        blobSelectScreen.SetActive(true);
        this.blobSelectObserver = blobSelectObserver;
        alreadySelectedBlobs = selectedBlobIds;
    }*/
}
