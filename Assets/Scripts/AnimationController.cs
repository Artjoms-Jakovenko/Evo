using System.Collections;
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

    void Awake()
    {
        blobAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (IsAnimationLocked())
        {
            lockTime -= Time.deltaTime;
        }
    }

    private void ResetAllStates()
    {
        foreach(var trigger in animationStates)
        {
            blobAnimator.ResetTrigger(trigger.Value);
        }
    }

    public void PlayAnimation(AnimationState animationState)
    {
        ResetAllStates();
        switch (animationState)
        {
            case AnimationState.Kick:
                lockTime = 1.25F;
                break;
        }
        blobAnimator.Play(animationStates[animationState]);
    }

    public bool IsAnimationLocked()
    {
        return lockTime > 0.0F;
    }
}
