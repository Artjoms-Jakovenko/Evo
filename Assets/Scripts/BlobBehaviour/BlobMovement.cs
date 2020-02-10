using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlobMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private BlobStats blobStats;
    private void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        blobStats = gameObject.GetComponent<BlobStats>();
    }
    public void RunTo(Vector3 targetLocation) // TODO rename to setdestination
    {
        navMeshAgent.SetDestination(targetLocation);
        navMeshAgent.isStopped = false;

        navMeshAgent.speed = blobStats.stats.stats[StatName.Speed].value; // TODO speed should be updated live inside speed component
    }

    public void LookTo(Transform runner, Transform targetLocation)
    {
        Vector3 movement = GetDirectionBetweenObjects(runner, targetLocation);
        runner.transform.rotation = Quaternion.LookRotation(movement); 
    }

    public void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
    }

    public void Wander() // TODO distance mask etc
    {
        /*Vector3 randomDirection = UnityEngine.Random.insideUnitCircle;

        randomDirection += gameObject.transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, 10, -1);

        navMeshAgent.SetDestination(navHit.position);
        navMeshAgent.isStopped = false;*/
    }

    private Vector3 GetDirectionBetweenObjects(Transform runner, Transform targetLocation)
    {
        Vector3 movement = targetLocation.position - runner.position;
        movement.y = 0.0F;
        movement = movement.normalized;

        return movement;
    }
}
