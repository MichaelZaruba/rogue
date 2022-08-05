using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour
{
    public string currentState;
    public Animator animator;

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;
        currentState = newState;
        animator.Play(newState);
    }
}
