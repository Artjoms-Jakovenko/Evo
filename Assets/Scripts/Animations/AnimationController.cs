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
    float lockTime = 0.0F;

    private AnimationState lockedState;
    private bool lockActionCompleted = false;

    void Awake()
    {
        blobAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (IsAnimationLocked())
        {
            lockTime -= Time.deltaTime; // TODO move into play animation can be deterministcally calculated
        }
    }

    public void PlayAnimation(AnimationState animationState)
    {
        if (!IsAnimationLocked())
        {
            blobAnimator.Play(animationStates[animationState]); // TODO isanimationlocked here
        }
    }

    public void PlayAnimation(MeleeFightAction meleeFightAction) // Workaround due to restriction to a single thread while Actions are not Monobehaviour and can't be invoked TODO remove
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

    public void KickAnimationEvent()
    {
        if(OnKicked != null)
        {
            OnKicked();
        }
    }
}
