﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator blobAnimator;
    Dictionary<AnimationState, string> animationStates = new Dictionary<AnimationState, string>()
    {
        { AnimationState.Idle, "Idle" },
        { AnimationState.Walk, "Walk" },
        { AnimationState.Kick, "Kick" },
    };
    float lockTime = 0.0F;

    private MeleeFightAction meleeFightAction;
    private AnimationState lockedState;
    private bool lockActionCompleted = false;

    void Start()
    {
        blobAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (IsAnimationLocked())
        {
            lockTime -= Time.deltaTime;

            if (!lockActionCompleted)
            {
                switch (lockedState)
                {
                    case AnimationState.Kick:
                        if (lockTime < 0.25F)
                        {
                            meleeFightAction.DealDamage();
                            lockActionCompleted = true;
                        }
                        break;
                }
            }
        }
    }

    public void PlayAnimation(AnimationState animationState)
    {
        blobAnimator.Play(animationStates[animationState]);
    }

    public void PlayAnimation(MeleeFightAction meleeFightAction) // Workaround due to restriction to a single thread while Actions are not Monobehaviour and can't be invoked
    {
        lockActionCompleted = false;
        this.meleeFightAction = meleeFightAction;
        lockTime = 1.25F;
        lockedState = AnimationState.Kick;
        blobAnimator.Play(animationStates[AnimationState.Kick]);
    }

    public bool IsAnimationLocked()
    {
        return lockTime > 0.0F;
    }
}
