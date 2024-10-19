using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayerState : ControllerState
{
    public override void SetState(AnimationStates newState)
    {
        if (state == newState)
            return;
        state = newState;
        if(state == AnimationStates.Walk)
            anim.SetTrigger("Walk");
        else if(state == AnimationStates.Run)
            anim.SetTrigger("Run");
        else if(state == AnimationStates.Jump)
            anim.SetTrigger("Jump");
        else if(state == AnimationStates.Attack)
            anim.SetTrigger("Attack");
        else if(state == AnimationStates.Dead)
            anim.SetTrigger("Dead");
        else if(state == AnimationStates.Climb)
            anim.SetTrigger("Climb");
        else if(state == AnimationStates.Fall)
            anim.SetTrigger("Fall");
        else if(state == AnimationStates.Hurt)
            anim.SetTrigger("Hurt");
        else
            anim.SetTrigger("Idle");
    }
}
