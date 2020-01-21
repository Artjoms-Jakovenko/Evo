using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMovement : MonoBehaviour
{
    private BlobStats blobStats;
    private void Awake()
    {
        blobStats = gameObject.GetComponent<BlobStats>();
    }
    public void RunAndLookTo(Transform runner, Transform targetLocation)
    {
        Vector3 movement = targetLocation.position - runner.position;
        movement.y = 0.0F;
        movement = movement.normalized;

        runner.transform.rotation = Quaternion.LookRotation(movement); 
        runner.transform.Translate(new Vector3(0.0F, 0.0F, Time.deltaTime * blobStats.stats.stats[StatName.Speed].value), Space.Self);
    }
}
