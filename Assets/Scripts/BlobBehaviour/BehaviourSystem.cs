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

    // EatAction
    

    private Dictionary<Action, IAction> ActionDictionary = new Dictionary<Action, IAction>();

    void Awake()
    {
        InitStats();
        InitActions();
    }

    #region Initialization
    void InitStats()
    {
        reactionPeriod = 1.0F;
        timeWithoutReaction = 0.0F;
        // Add stats to blob stats based on saved data
    }

    void InitActions()
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
    }

    private void FixedUpdate()
    {
        ActionDictionary[CurrentStrategy].PerformAction();
    }

    private Action GetBestAction()
    {
        Action bestAction = Action.None; // TODO
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
