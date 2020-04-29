using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlobSelector : MonoBehaviour
{
    public GameObject blobSelectionBar;
    public LevelInfo levelInfo;
    public Button startRoundButton;
    public GameObject sliderContent;
    public Transform spawnPoints;

    List<GameObject> blobButtons = new List<GameObject>();

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

        for (int i = 0; i < SaveSystem.saveData.blobData.Count; i++)
        {
            GetObjectAt(i);
        }

        SetStartButtonInteractability();
    }

    private GameObject GetObjectAt(int position)
    {
        GameObject blobButton = GameObjectUtility.InstantiateChild(blobAddButton, sliderContent, true);
        BlobDragSpawnerUi blobDragSpawnerUi = blobButton.GetComponent<BlobDragSpawnerUi>();

        GameObject blobPlaceholder = Instantiate(UiData.blobAssets[SaveSystem.saveData.blobData[position].blobType].Asset);
        blobPlaceholder.transform.parent = spawnPoints;

        blobDragSpawnerUi.SetAssociatedBlob(blobPlaceholder);

        int buttonID = blobButtons.Count;
        blobButtons.Add(blobButton);

        return blobButton;
    }

    void DeselectBlob(int buttonId)
    {
        SetStartButtonInteractability();
    }

    public void SelectedBlob(int blobID)
    {
        SetStartButtonInteractability();
    }

    public List<int> GetSelectedBlobIds()
    {
        return new List<int>(); // TODO
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
