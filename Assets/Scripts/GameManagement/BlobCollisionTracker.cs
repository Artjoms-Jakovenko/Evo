using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobCollisionTracker : MonoBehaviour
{
    public bool IsColliding(int layerMask) // TODO
    {
        if (Physics.OverlapSphere(transform.position, 0.25F, layerMask).Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
