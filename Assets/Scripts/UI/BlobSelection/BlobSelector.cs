using Assets.Scripts.UI;
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
    LinearSlider linearSlider;

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

        List<GameObject> blobAddButtons = new List<GameObject>();
        for (int i = 0; i < levelInfo.maxBlobCount; i++)
        {
            blobAddButtons.Add(GetObjectAt(i));
        }
        linearSlider = new LinearSlider(sliderContent);
        linearSlider.RenderSliderElements(blobAddButtons);

        SetStartButtonInteractability();
    }

    private GameObject GetObjectAt(int position)
    {
        GameObject blobButton = GameObjectUtility.InstantiateChild(blobAddButton, sliderContent, true);

        int buttonID = blobButtons.Count;
        blobButtons.Add(blobButton);
        // Add events to buttons
        blobButton.GetComponent<Button>().onClick.AddListener(() => SelectButtonClicked(buttonID));

        return blobButton;
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
        selectedBlobIds.Add(blobID); // TODO also make possible to remove blobs

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
