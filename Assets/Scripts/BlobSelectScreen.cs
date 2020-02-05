using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlobSelectScreen : MonoBehaviour
{
    public delegate void BlobSelected(int blobId);
    public static event BlobSelected OnBlobSelected;

    public GameObject blobSelectScreen; // TODO move this script to blobselectscreen

    private Dictionary<int, GameObject> buttons = new Dictionary<int, GameObject>();

    // Workaround to access dictionary by index, since dictionary element order is undefined
    List<int> blobDataKeys;

    void Start()
    {
        SaveData saveData = SaveSystem.Load();

        // Sort blobDataKeys
        blobDataKeys = new List<int>(saveData.blobData.Keys);
        blobDataKeys.Sort(); // Caused performance issues TODO

        float containerHeight = saveData.blobData.Count * 300.0F;
        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(containerHeight, 0.0F, 200.0F, saveData.blobData.Count);

        GameObject blobBarAsset = Resources.Load("TempButton") as GameObject; // TODO

        for (int i = 0; i < blobDataKeys.Count; i++)
        {
            // Create button
            GameObject blobBar = GameObjectUtility.InstantiateChild(blobBarAsset, gameObject, true);
            blobBar.GetComponentInChildren<TextMeshProUGUI>().text = blobDataKeys[i].ToString();

            // Place button
            RectTransform blobBarRectTransform = blobBar.GetComponent<RectTransform>();
            blobBarRectTransform.transform.localPosition = new Vector3(0.0F, linearUiSpacing.GetNthPathPosition(i));

            // Add events to buttons
            int associatedBlobId = blobDataKeys[i];
            blobBar.GetComponent<Button>().onClick.AddListener(() => BlobClicked(associatedBlobId));

            // Add button to the dictionary
            buttons.Add(blobDataKeys[i], blobBar);
        }
    }

    void BlobClicked(int blobId)
    {
        OnBlobSelected(blobId);
        blobSelectScreen.SetActive(false);
    }

    public void SelectBlob()
    {
        blobSelectScreen.SetActive(true);
        //alreadySelectedBlobs = new List<int>();
    }

    public void SelectBlob(List<int> selectedBlobIds)
    {
        blobSelectScreen.SetActive(true);
        foreach (var selectedBlobId in selectedBlobIds)
        {
            buttons[selectedBlobId].SetActive(false); // TODO dont remove, but make unclickable with text over them
        }
    }
}
