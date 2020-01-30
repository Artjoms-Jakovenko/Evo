using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlobSelector : SliderSelector
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject blobSelectionBar;
    public BlobSelectScreen blobSelectScreen;

    List<int> selectedBlobIds = new List<int>();

    List<GameObject> blobButtons = new List<GameObject>();
    int selectedButton = 0;

    GameObject blobAddButton;

    int blobCount = 2; // TODO max allowed blobs on this level

    private void OnEnable()
    {
        BlobSelectScreen.OnBlobSelected += SelectedBlob;
    }

    private void OnDisable()
    {
        BlobSelectScreen.OnBlobSelected -= SelectedBlob;
    }

    public override GameObject GetObjectAt(int position)
    {
        GameObject blobButton = GameObjectUtility.InstantiateChild(blobAddButton, gameObject, true);

        int buttonID = blobButtons.Count;
        blobButtons.Add(blobButton);
        // Add events to buttons
        blobButton.GetComponent<Button>().onClick.AddListener(() => SelectButtonClicked(buttonID));

        return blobButton;
    }

    public override int GetObjectCount()
    {
        return blobCount;
    }

    void Start()
    {
        blobAddButton = Resources.Load("UI/BlobSelect/AddBlobButton") as GameObject;
        RectTransform blobAddButtonRectTransform = blobAddButton.GetComponent<RectTransform>();

        SaveData saveData = SaveSystem.Load();
        blobCount = saveData.blobData.Count;

        LinearUiSpacing linearUiSpacing = new LinearUiSpacing(blobSelectionBar.GetComponent<RectTransform>().rect.width, 80.0F, blobAddButtonRectTransform.rect.width, 20.0F);
        base.Initialize(leftArrow, rightArrow, linearUiSpacing);
        base.RenderSliderElements();
    }

    void SelectButtonClicked(int buttonID)
    {
        Debug.Log("Button " + buttonID);
        blobSelectScreen.SelectBlob(selectedBlobIds);
        selectedButton = buttonID;
    }

    public void SelectedBlob(int blobID)
    {
        blobButtons[selectedButton].GetComponent<AddBlobButton>().SwitchToSelectedBlob(BlobType.Survivor, "Blobby" + blobID); // TODO
        selectedBlobIds.Add(blobID); // TODO also remove
    }

    public List<int> GetSelectedBlobIds()
    {
        return selectedBlobIds; // TODO
    }
}
