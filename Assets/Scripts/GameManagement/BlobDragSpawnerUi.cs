using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlobDragSpawnerUi : MonoBehaviour, IPointerDownHandler
{
    public GameObject blobIcon; 
    private GameObject associatedBlob;

    [HideInInspector]
    public bool dragged = false;

    private BlobDragSpawner blobDragSpawner;
    

    void Start()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragged = true;
    }

    public void Placed()
    {
        if (blobDragSpawner.isColliding)
        {
            blobIcon.SetActive(true);
            associatedBlob.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && dragged)
        {
            blobIcon.transform.position = Input.mousePosition;

            if (blobDragSpawner.PlaceAtRaycastOnNavmesh())
            {
                blobIcon.SetActive(false);
                associatedBlob.SetActive(true);
            }
            else
            {
                blobIcon.SetActive(true);
                associatedBlob.SetActive(false);
            }
        }
        else
        {
            if (dragged) // Will be true on the frame when dragging is released
            {
                if (blobDragSpawner.isColliding)
                {
                    blobIcon.SetActive(true);
                    associatedBlob.SetActive(false);
                }
            }
            ReturnToBackground();
        }
    }

    public void SetAssociatedBlob(int blobId, GameObject blob)
    {
        associatedBlob = blob;
        blobDragSpawner = associatedBlob.GetComponent<BlobDragSpawner>();
        blobDragSpawner.blobDragSpawnerUi = this;
        blobDragSpawner.associatedBlobId = blobId;
        SetIcon(SaveSystem.saveData.blobData[blobId].blobType);
    }

    private void ReturnToBackground()
    {
        blobIcon.transform.position = gameObject.transform.position;
        dragged = false;
    }

    private void SetIcon(BlobType blobType)
    {
        transform.Find("BlobIcon").GetComponent<Image>().sprite = UiData.blobAssets[blobType].Icon;
    }
}
