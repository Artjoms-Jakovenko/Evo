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

    public GameObject blobContainer; // TODO move this script to blobselectscreen

    private readonly Dictionary<int, GameObject> buttons = new Dictionary<int, GameObject>();

    // Workaround to access dictionary by index, since dictionary element order is undefined
    List<int> blobDataKeys;

    void Start()
    {
        SaveData saveData = SaveSystem.Load();

        // Sort blobDataKeys
        blobDataKeys = new List<int>(saveData.blobData.Keys);
        blobDataKeys.Sort();

        float containerHeight = saveData.blobData.Count * 300.0F;
        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(containerHeight, 0.0F, 200.0F, saveData.blobData.Count);

        GameObject blobBarAsset = Resources.Load("TempButton") as GameObject; // TODO

        for (int i = 0; i < blobDataKeys.Count; i++)
        {
            // Create button
            GameObject blobBar = GameObjectUtility.InstantiateChild(blobBarAsset, blobContainer, true);
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
        gameObject.SetActive(false);
    }

    public void SelectBlob()
    {
        gameObject.SetActive(true);
        //alreadySelectedBlobs = new List<int>();
    }

    public void SelectBlob(List<int> selectedBlobIds)
    {
        gameObject.SetActive(true);
        foreach (var selectedBlobId in selectedBlobIds)
        {
            buttons[selectedBlobId].SetActive(false); // TODO dont remove, but make unclickable with text over them
        }
    }
}
