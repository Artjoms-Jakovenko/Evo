using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSelector : SliderSelector, IBlobSelectObserver
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject blobSelectionBar;
    public BlobSelectScreen blobSelectScreen;

    List<int> selectedBlob = new List<int>();

    GameObject blobAddButton;

    int blobCount = 2; // TODO max allowed blobs on this level
    public override GameObject GetObjectAt(int position)
    {
        GameObject statBackgroundGameObject = GameObjectUtility.InstantiateChild(blobAddButton, gameObject, true);

        return statBackgroundGameObject;
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
        blobSelectScreen.SelectBlob(this); // TODO move on button click
    }

    public void SelectedBlob(int blobID)
    {
        Debug.Log(blobID);
        throw new System.NotImplementedException();
    }
}
