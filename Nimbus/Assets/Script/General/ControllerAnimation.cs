using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerState : MonoBehaviour
{
    protected AnimationStates state;
    protected Animator anim;

    private void Awake()
    {
        state = AnimationStates.Idle;
        anim = GetComponent<Animator>();
    }

    public virtual void SetState(AnimationStates newState){
        if (state == newState)
            return;
        state = newState;
    }
}
