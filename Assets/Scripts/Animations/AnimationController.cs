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

    private void Update()
    {
        if (IsAnimationLocked())
        {
            lockTime -= Time.deltaTime;
        }
    }

    void Awake()
    {
        blobAnimator = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationState animationState)
    {
        if (!IsAnimationLocked() || animationState == AnimationState.Death)
        {
            blobAnimator.Play(animationStates[animationState]);
            blobAnimator.Update(0.0F); // Have to call this, because otherwise does not register stateInfo change
        }

        switch (animationState)
        {
            case AnimationState.Death:
                lockTime = float.MaxValue;
                break;
            case AnimationState.Kick:
                lockTime = 1.25F;
                break;
        }
    }

    public bool IsAnimationLocked()
    {
        return lockTime > 0.0F;
    }

    #region Animation events

    public void KickAnimationEvent()
    {
        OnKicked?.Invoke();
    }

    public void DeathAnimationEvent()
    {
        Destroy(gameObject);
    }

    #endregion
}
