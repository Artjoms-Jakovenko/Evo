using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Eat,
    None,
}

public class BehaviourSystem : MonoBehaviour
{
    private float reactionPeriod;
    private float timeWithoutReaction;
    private Action CurrentStrategy = Action.None;

    private Dictionary<Action, IAction> ActionDictionary = new Dictionary<Action, IAction>();

    /*void OnDrawGizmos() // To test colliders
    {
        Gizmos.color = Color.red;
        Vector3 position = transform.position;
        position.y += transform.localScale.y / 2;
        Gizmos.DrawSphere(position, transform.localScale.x / 2);
    }*/
    void Awake()
    {
        InitStats();
        InitActions();
    }

    #region Initialization
    void InitStats() // TODO Move it to blob instantiator
    {
        reactionPeriod = 5.0F;
        timeWithoutReaction = 0.0F;
        // Add stats to blob stats based on saved data
    }

    void InitActions() // TODO Move it to blob instantiator
    {
        ActionDictionary.Add(Action.None, new NoneAction());

        List<List<ObjectTag>> edibleTagCombinations = new List<List<ObjectTag>>()
        {
            new List<ObjectTag>(){ ObjectTag.Edible, ObjectTag.Small, ObjectTag.Plant }
        };
        // TODO stat initialization based on required stats
        // TODO addcomponent hunger, init hunger
        ActionDictionary.Add(Action.Eat, new EatAction(gameObject.transform, edibleTagCombinations));
    }
    #endregion

    void Update()
    {
        timeWithoutReaction += Time.deltaTime;
        if(timeWithoutReaction > reactionPeriod)
        {
            timeWithoutReaction -= reactionPeriod;
            CurrentStrategy = GetBestAction();
            ActionDictionary[CurrentStrategy].MakeDecision();
        }

        ActionDictionary[CurrentStrategy].PerformAction();
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
