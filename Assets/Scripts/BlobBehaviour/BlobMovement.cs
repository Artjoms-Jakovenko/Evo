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
        Vector3 movement = GetDirectionBetweenObjects(runner, targetLocation);

        runner.transform.rotation = Quaternion.LookRotation(movement); 
        runner.transform.Translate(new Vector3(0.0F, 0.0F, Time.deltaTime * blobStats.stats.stats[StatName.Speed].value), Space.Self);
    }

    public void LookTo(Transform runner, Transform targetLocation)
    {
        Vector3 movement = GetDirectionBetweenObjects(runner, targetLocation);
        runner.transform.rotation = Quaternion.LookRotation(movement); 
    }

    private Vector3 GetDirectionBetweenObjects(Transform runner, Transform targetLocation)
    {
        Vector3 movement = targetLocation.position - runner.position;
        movement.y = 0.0F;
        movement = movement.normalized;

        return movement;
    }
}
