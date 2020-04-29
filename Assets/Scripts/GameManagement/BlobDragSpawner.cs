using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.BaseShaderGUI;

public class BlobDragSpawner : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private BlobCollisionTracker collisionTracker;

    private Camera mainCamera;
    private GameObject blob;
    private GameObject blobIcon;
    public BlobDragSpawnerUi blobDragSpawnerUi;

    private Color32 initialColor;

    public bool isColliding = false;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        initialColor = skinnedMeshRenderer.material.color;
    }

    private void Start()
    {
        collisionTracker = GetComponent<BlobCollisionTracker>();
        gameObject.SetActive(false); // disable itself after initialization
    }

    private void Update()
    {
        isColliding = collisionTracker.IsColliding(0x0100);
        if (isColliding) // Layer 8 (0x0100) for tagged ogjects
        {
            //skinnedMeshRenderer.material.SetFloat("_Surface", (float)SurfaceType.Transparent);
            skinnedMeshRenderer.material.color = new Color32(255, 0, 0, 127);
        }
        else
        {
            skinnedMeshRenderer.material.color = initialColor;
        }
    }

    private void OnMouseDown()
    {
        blobDragSpawnerUi.dragged = true;
    }

    // True if raycast hits navmesh, false if does not
    public bool PlaceAtRaycastOnNavmesh()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        bool raycastHitNavmesh = Physics.Raycast(ray, out hit, 1000.0F, 0x0400);

        if (raycastHitNavmesh)// 0x0400 for navmesh mask (layer 10)
        {
            Physics.autoSimulation = false; // Workaround to be able to raycast with TimeScale = 0
            gameObject.transform.position = hit.point;
            Physics.Simulate(0.01F);
            Physics.autoSimulation = true;
        }

        return raycastHitNavmesh;
    }
}
