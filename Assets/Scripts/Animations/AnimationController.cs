using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public delegate void AnimationEventHappened();
    public event AnimationEventHappened OnKicked;

    Animator blobAnimator;
    Dictionary<AnimationState, string> animationStates = new Dictionary<AnimationState, string>()
    {
        { AnimationState.Idle, "Idle" },
        { AnimationState.Walk, "Walk" },
        { AnimationState.Kick, "Kick" },
        { AnimationState.Death, "Death" },
    };

    void Awake()
    {
        blobAnimator = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationState animationState)
    {
        if (!IsAnimationLocked())
        {
            blobAnimator.Play(animationStates[animationState]);
        }
    }

    public bool IsAnimationLocked()
    {
        AnimatorStateInfo stateInfo = blobAnimator.GetCurrentAnimatorStateInfo(0); // Base layer 0
        return !stateInfo.loop;
    }

    public void KickAnimationEvent()
    {
        OnKicked?.Invoke();
    }
}
