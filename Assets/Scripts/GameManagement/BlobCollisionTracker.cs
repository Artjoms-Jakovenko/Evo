using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobCollisionTracker : MonoBehaviour
{
    public bool IsColliding(int layerMask) // TODO
    {
        if (Physics.OverlapSphere(transform.position, 1.0F, layerMask).Length > 1) // > 1 is accounting for the object itself, which will be hit by overlapSphere 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
