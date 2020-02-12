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

    public GameObject blobContent;

    private readonly Dictionary<int, GameObject> buttons = new Dictionary<int, GameObject>();

    // Workaround to access dictionary by index, since dictionary element order is undefined
    List<int> blobDataKeys;
    GameObject blobBarAsset;

    private void Awake()
    {
        blobBarAsset = Resources.Load("TempButton") as GameObject; // TODO
    }

    void Start()
    {
        SaveData saveData = SaveSystem.Load();

        // Sort blobDataKeys
        blobDataKeys = new List<int>(saveData.blobData.Keys);
        blobDataKeys.Sort();

        for (int i = 0; i < blobDataKeys.Count; i++)
        {
            GetObjectAt(i);
        }
    }

    private GameObject GetObjectAt(int position)
    {
        // Create button
        GameObject blobBar = GameObjectUtility.InstantiateChild(blobBarAsset, blobContent, true);
        blobBar.GetComponentInChildren<TextMeshProUGUI>().text = blobDataKeys[position].ToString();

        // Add events to buttons
        int associatedBlobId = blobDataKeys[position];
        blobBar.GetComponent<Button>().onClick.AddListener(() => BlobClicked(associatedBlobId));

        // Add button to the dictionary
        buttons.Add(blobDataKeys[position], blobBar);

        return blobBar;
    }

    void BlobClicked(int blobId)
    {
        OnBlobSelected(blobId);
        gameObject.SetActive(false);
    }

    public void SelectBlob()
    {
        gameObject.SetActive(true);
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
