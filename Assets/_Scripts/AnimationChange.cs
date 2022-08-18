using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour
{
    public string currentState;
    [HideInInspector]
    public Animator Animator;

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;
        currentState = newState;
        Animator.Play(newState);
    }
}
