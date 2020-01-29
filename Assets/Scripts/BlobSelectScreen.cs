using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlobSelectScreen : MonoBehaviour
{
    public GameObject blobSelectScreen;

    private IBlobSelectObserver blobSelectObserver;
    private Dictionary<int, GameObject> buttons = new Dictionary<int, GameObject>();

    // Workaround to access dictionary by index, since dictionary element order is undefined
    List<KeyValuePair<int, BlobStatsData>> blobData;

    void Start()
    {
        SaveData saveData = SaveSystem.Load();

        // Sort keyValuePairs
        blobData = saveData.blobData.ToList();
        blobData.Sort((x, y) => x.Key.CompareTo(y.Key));

        float containerHeight = saveData.blobData.Count * 300.0F;
        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(containerHeight, 0.0F, 200.0F, saveData.blobData.Count);

        GameObject blobBarAsset = Resources.Load("TempButton") as GameObject; // TODO

        for (int i = 0; i < blobData.Count; i++)
        {
            // Create button
            GameObject blobBar = GameObjectUtility.InstantiateChild(blobBarAsset, gameObject, true);
            blobBar.GetComponentInChildren<TextMeshProUGUI>().text = blobData[i].Key.ToString();

            // Place button
            RectTransform blobBarRectTransform = blobBar.GetComponent<RectTransform>();
            blobBarRectTransform.transform.localPosition = new Vector3(0.0F, linearUiSpacing.GetNthPathPosition(i));

            // Add events to buttons
            int associatedBlobId = blobData[i].Key;
            blobBar.GetComponent<Button>().onClick.AddListener(() => BlobClicked(associatedBlobId));

            // Add button to the dictionary
            buttons.Add(blobData[i].Key, blobBar);
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

    public void SelectBlob(IBlobSelectObserver blobSelectObserver, List<int> selectedBlobIds)
    {
        blobSelectScreen.SetActive(true);
        this.blobSelectObserver = blobSelectObserver;
        foreach (var selectedBlobId in selectedBlobIds)
        {
            buttons[selectedBlobId].SetActive(false); // TODO dont remove, but make unclickable with text over them
        }
    }
}
