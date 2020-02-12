using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlobMovement : MonoBehaviour // TODO consider adding target reached events
{
    private NavMeshAgent navMeshAgent;
    private Speed speed;
    private Sight sight;
    private AnimationController blobAnimationController;

    private void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        speed = gameObject.GetComponent<Speed>();
        sight = gameObject.GetComponent<Sight>();
        blobAnimationController = gameObject.GetComponent<AnimationController>();
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance < 0.1f)
        {
            blobAnimationController.PlayAnimation(AnimationState.Idle);
        }
    }

    private void Start()
    {
        navMeshAgent.speed = 0.0F;
    }

    private void OnEnable()
    {
        speed.OnSpeedChanged += UpdateSpeed;
    }

    private void OnDisable()
    {
        speed.OnSpeedChanged -= UpdateSpeed;
    }

    private void UpdateSpeed()
    {
        navMeshAgent.speed = speed.GetSpeed();
    }

    public void RunTo(Vector3 targetLocation) // TODO rename to setdestination
    {
        navMeshAgent.SetDestination(targetLocation);
        navMeshAgent.isStopped = false;
        blobAnimationController.PlayAnimation(AnimationState.Walk);
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
        blobAnimationController.PlayAnimation(AnimationState.Idle); // Presumably this doesnt work
    }

    public void SetSpeed(float speed)
    {
        navMeshAgent.speed = speed;
    }

    public void StartWandering()
    {
        Vector2 randomDestination = Random.insideUnitCircle;
        navMeshAgent.SetDestination(sight.GetSight() * new Vector3(randomDestination.x, 0.0F, randomDestination.y) + gameObject.transform.position);
        navMeshAgent.isStopped = false;
        blobAnimationController.PlayAnimation(AnimationState.Walk);
    }

    private Vector3 GetDirectionBetweenObjects(Transform runner, Transform targetLocation)
    {
        Vector3 movement = targetLocation.position - runner.position;
        movement.y = 0.0F;
        movement = movement.normalized;

        return movement;
    }
}
