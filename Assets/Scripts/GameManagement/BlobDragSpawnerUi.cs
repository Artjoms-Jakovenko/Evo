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
    public int associatedBlobId;
    [HideInInspector]
    public bool dragged = false;

    private BlobDragSpawner blobDragSpawner;
    

    void Start()
    {
        blobDragSpawner = associatedBlob.GetComponent<BlobDragSpawner>();
        blobDragSpawner.blobDragSpawnerUi = this;
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
            if (dragged)
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

    public void SetAssociatedBlob(GameObject blob)
    {
        associatedBlob = blob;
    }

    private void ReturnToBackground()
    {
        blobIcon.transform.position = gameObject.transform.position;
        dragged = false;
    }
}
