using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlobSelector : MonoBehaviour
{
    public GameObject blobSelectionBar;
    public BlobSelectScreen blobSelectScreen;
    public LevelInfo levelInfo;
    public Button startRoundButton;
    public GameObject sliderContent;

    List<int> selectedBlobIds = new List<int>();

    List<GameObject> blobButtons = new List<GameObject>();
    int selectedButton = 0;

    GameObject blobAddButton;

    private void OnEnable()
    {
        BlobSelectScreen.OnBlobSelected += SelectedBlob;
    }

    private void OnDisable()
    {
        BlobSelectScreen.OnBlobSelected -= SelectedBlob;
    }    

    void Start()
    {
        blobAddButton = Resources.Load("UI/BlobSelect/AddBlobButton") as GameObject;
        RectTransform blobAddButtonRectTransform = blobAddButton.GetComponent<RectTransform>();

        for (int i = 0; i < levelInfo.maxBlobCount; i++)
        {
            GetObjectAt(i);
        }

        SetStartButtonInteractability();
    }

    private GameObject GetObjectAt(int position)
    {
        GameObject blobButton = GameObjectUtility.InstantiateChild(blobAddButton, sliderContent, true);

        int buttonID = blobButtons.Count;
        blobButtons.Add(blobButton);
        // Add events to buttons
        blobButton.GetComponent<Button>().onClick.AddListener(() => SelectButtonClicked(buttonID));

        foreach (Button button in blobButton.GetComponentsInChildren<Button>(true))
        {
            if (button.gameObject.GetInstanceID() != blobButton.GetInstanceID())
            {
                button.onClick.AddListener(() => DeselectBlob(buttonID));
                break;
            }
        }

        return blobButton;
    }

    void SelectButtonClicked(int buttonId)
    {
        Debug.Log("Button " + buttonId);
        blobSelectScreen.SelectBlob(selectedBlobIds);
        selectedButton = buttonId;
    }

    void DeselectBlob(int buttonId)
    {
        AddBlobButton addBlobButton = blobButtons[buttonId].GetComponent<AddBlobButton>();
        addBlobButton.SwitchToPlusSign();
        selectedBlobIds.RemoveAll(x => x == addBlobButton.buttonBlobId);

        SetStartButtonInteractability();
    }

    public void SelectedBlob(int blobID)
    {
        AddBlobButton addBlobButton = blobButtons[selectedButton].GetComponent<AddBlobButton>();

        if (addBlobButton.HasBlobSelected())
        {
            selectedBlobIds.RemoveAll(x => x == addBlobButton.buttonBlobId);
        }

        addBlobButton.SwitchToSelectedBlob(BlobType.Survivor, blobID); // TODO
        selectedBlobIds.Add(blobID);

        SetStartButtonInteractability();
    }

    public List<int> GetSelectedBlobIds()
    {
        return selectedBlobIds;
    }

    private void SetStartButtonInteractability()
    {
        if (GetSelectedBlobIds().Count > 0)
        {
            startRoundButton.interactable = true;
        }
        else
        {
            startRoundButton.interactable = false;
        }
    }
}
