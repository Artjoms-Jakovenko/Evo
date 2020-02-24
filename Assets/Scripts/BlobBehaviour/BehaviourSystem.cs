using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSystem : MonoBehaviour
{
    private float reactionPeriod;
    private float timeWithoutReaction;
    private ActionEnum CurrentStrategy = ActionEnum.None;
    private AnimationController animationController;
    private BlobStats blobStats;

    private Dictionary<ActionEnum, IAction> ActionDictionary = new Dictionary<ActionEnum, IAction>();

    public delegate void ActionChanged(ActionEnum actionEnum);
    public event ActionChanged OnActionChanged;

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
    void InitStats()
    {
        reactionPeriod = blobStats.stats.stats[StatName.ReactionTime].value;
        timeWithoutReaction = 0.0F;
    }

    void InitActions()
    {
        foreach(var action in blobStats.stats.possibleActions)
        {
            AddAction(action);
        }
    }

    void AddAction(ActionEnum action)
    {
        switch (action)
        {
            case ActionEnum.None:
                ActionDictionary.Add(ActionEnum.None, new NoneAction(gameObject));
                break;
            case ActionEnum.Eat:
                ActionDictionary.Add(ActionEnum.Eat, new EatAction(gameObject, blobStats.stats.edibleTagCombinations));
                break;
            case ActionEnum.MeleeFight:
                ActionDictionary.Add(ActionEnum.MeleeFight, new MeleeFightAction(gameObject));
                break;
            case ActionEnum.RunAway:
                ActionDictionary.Add(ActionEnum.RunAway, new RunAwayAction(gameObject));
                break;
        }
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
                OnActionChanged(CurrentStrategy);
                ActionDictionary[CurrentStrategy].MakeDecision();
            }

            ActionDictionary[CurrentStrategy].PerformAction();
        }
    }

    private ActionEnum GetBestAction()
    {
        ActionEnum bestAction = ActionEnum.None;
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
