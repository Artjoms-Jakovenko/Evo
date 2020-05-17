using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragManager : MonoBehaviour, IPointerDownHandler
{
    public GameObject asset;
    public Image objectIcon;
    public Camera mainCamera;

    private bool dragged = false;
    private GameObject objectInstance = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        dragged = true;
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && dragged)
        {
            objectIcon.transform.position = Input.mousePosition;
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            bool raycastHitNavmesh = Physics.Raycast(ray, out hit, 100000.0F, 0x0400);

            if (raycastHitNavmesh)// 0x0400 for navmesh mask (layer 10)
            {
                if(objectInstance == null)
                {
                    objectInstance = Instantiate(asset);
                    ObjectManager.GetInstance().AddObject(objectInstance);
                }
                objectInstance.transform.position = hit.point;
                objectIcon.enabled = false;
            }
            else
            {

            }
        }
        else
        {
            if (dragged)
            {
                objectInstance = null;
            }
            ReturnToBackground();
        }
    }

    private void ReturnToBackground()
    {
        objectIcon.transform.position = gameObject.transform.position;
        dragged = false;
        objectIcon.enabled = true;
    }

    public bool PlaceAtRaycastOnNavmesh()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        bool raycastHitNavmesh = Physics.Raycast(ray, out hit, 1000.0F, 0x0400);

        if (raycastHitNavmesh)// 0x0400 for navmesh mask (layer 10)
        {
            gameObject.transform.position = hit.point;
        }

        return raycastHitNavmesh;
    }
}
