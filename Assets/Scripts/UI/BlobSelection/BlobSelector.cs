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
    public Transform playerSpawnPoints;

    List<GameObject> blobButtons = new List<GameObject>();

    GameObject blobAddButton;

    void Start()
    {
        blobAddButton = Resources.Load("UI/BlobSelect/AddBlobButton") as GameObject;

        for (int i = 0; i < SaveSystem.saveData.blobData.Count; i++)
        {
            GetObjectAt(i);
        }

        SetStartButtonInteractability();
    }

    private GameObject GetObjectAt(int position) // TODO position shall be dictionary key
    {
        GameObject blobButton = GameObjectUtility.InstantiateChild(blobAddButton, sliderContent, true);
        BlobDragSpawnerUi blobDragSpawnerUi = blobButton.GetComponent<BlobDragSpawnerUi>();

        GameObject blobPlaceholder = Instantiate(UiData.blobAssets[SaveSystem.saveData.blobData[position].blobType].Asset);
        blobPlaceholder.transform.parent = playerSpawnPoints;

        blobDragSpawnerUi.SetAssociatedBlob(position, blobPlaceholder);

        int buttonID = blobButtons.Count;
        blobButtons.Add(blobButton);

        return blobButton;
    }

    public Dictionary<int, Vector3> GetSelectedBlobIds()
    {
        Dictionary<int, Vector3> selectedBlobTransforms = new Dictionary<int, Vector3>();

        foreach (Transform blobTransform in playerSpawnPoints.transform)
        {
            BlobDragSpawner blobDragSpawner = blobTransform.gameObject.GetComponent<BlobDragSpawner>();
            if (blobTransform.gameObject.activeSelf && !blobDragSpawner.isColliding)
            {
                selectedBlobTransforms.Add(blobDragSpawner.associatedBlobId, blobTransform.position);
            }
        }

        return selectedBlobTransforms;
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

    private void Update()
    {
        SetStartButtonInteractability();
    }
}
