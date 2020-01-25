using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Eat,
    None,
    MeleeFight,
}

public class BehaviourSystem : MonoBehaviour
{
    private float reactionPeriod;
    private float timeWithoutReaction;
    private Action CurrentStrategy = Action.None;
    private AnimationController animationController;
    private BlobStats blobStats;

    private Dictionary<Action, IAction> ActionDictionary = new Dictionary<Action, IAction>();

    /*void OnDrawGizmos() // To test colliders
    {
        Gizmos.color = Color.red;
        Vector3 position = transform.position;
        position.y += transform.localScale.y / 2;
        Gizmos.DrawSphere(position, transform.localScale.x / 2);
    }*/

    void Start()
    {
        animationController = gameObject.GetComponent<AnimationController>();
        blobStats = GetComponent<BlobStats>();
        InitStats();
        InitActions();
    }

    #region Initialization
    void InitStats() // TODO Move it to blob instantiator
    {
        reactionPeriod = blobStats.stats.stats[StatName.ReactionTime].value;
        timeWithoutReaction = 0.0F;
        // Add stats to blob stats based on saved data
    }

    void InitActions() // TODO Move it to blob instantiator
    {
        ActionDictionary.Add(Action.None, new NoneAction(gameObject));
        ActionDictionary.Add(Action.Eat, new EatAction(gameObject.transform, blobStats.stats.edibleTagCombinations));
        ActionDictionary.Add(Action.MeleeFight, new MeleeFightAction(gameObject.transform));
    }
    #endregion

    void Update()
    {
        if (!animationController.IsAnimationLocked()) // Checks if busy performing some action
        {
            timeWithoutReaction += Time.deltaTime;
            if (timeWithoutReaction > reactionPeriod)
            {
                timeWithoutReaction -= reactionPeriod;
                CurrentStrategy = GetBestAction();
                ActionDictionary[CurrentStrategy].MakeDecision();
            }

            ActionDictionary[CurrentStrategy].PerformAction();
        }
    }

    private void FixedUpdate()
    {

    }

    private Action GetBestAction()
    {
        Action bestAction = Action.None;
        float bestActionScore = float.MinValue;
        foreach (var potentialAction in ActionDictionary)
        {
            float actionScore = potentialAction.Value.GetActionPriorityScore();
            if (actionScore > bestActionScore)
            {
                bestActionScore = actionScore;
                bestAction = potentialAction.Key;
            }
        }

        return bestAction;
    }
}
