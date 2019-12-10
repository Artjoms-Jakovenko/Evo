﻿using System.Collections;
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
    }

    void InitActions()
    {
        ActionDictionary.Add(Action.None, new NoneAction());

        List<List<ObjectTag>> edibleTagCombinations = new List<List<ObjectTag>>()
        {
            new List<ObjectTag>(){ ObjectTag.Edible, ObjectTag.Small, ObjectTag.Plant }
        };
        ActionDictionary.Add(Action.Eat, new EatAction(gameObject.transform, edibleTagCombinations));
    }
    #endregion

    void Update()
    {
        ActionDictionary[CurrentStrategy].PerformAction();

        timeWithoutReaction += Time.deltaTime;
        if(timeWithoutReaction > reactionPeriod)
        {
            timeWithoutReaction -= reactionPeriod;
            CurrentStrategy = GetBestAction();
            ActionDictionary[CurrentStrategy].MakeDecision();
        }
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
