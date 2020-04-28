using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlobDragSpawner : MonoBehaviour
{
    public Camera mainCamera;
    private BlobCollisionTracker collisionTracker;

    private void Start()
    {
        collisionTracker = GetComponent<BlobCollisionTracker>();
    }

    private void OnMouseDrag()
    {
        Debug.Log("Dragging");
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Physics.autoSimulation = false; // Workaround to be able to raycast with TimeScale = 0

        if (Physics.Raycast(ray, out hit, 1000.0F, 0x0400))// 0x0400 for navmesh mask (layer 10)
        {
            gameObject.transform.position = hit.point;
        }

        Physics.Simulate(0.01F);
        Physics.autoSimulation = true;
    }

    private void Update()
    {
        if (collisionTracker.IsColliding(0x0100)) // Layer 8 (0x0100) for tagged ogjects
        {

        }
    }
}
