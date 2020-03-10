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

    bool animationLocked = false;

    void Awake()
    {
        blobAnimator = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationState animationState)
    {
        AnimatorStateInfo stateInfo = blobAnimator.GetCurrentAnimatorStateInfo(0); // Base layer 0
        animationLocked = !stateInfo.loop;

        if (!IsAnimationLocked() || animationState == AnimationState.Death)
        {
            blobAnimator.Play(animationStates[animationState]);
            blobAnimator.Update(0.0F); // Have to call this, because otherwise does not register stateInfo change
        }
    }

    public bool IsAnimationLocked()
    {
        return animationLocked;
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
