using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayerState : MonoBehaviour
{
    private PlayerState state;
    private Animator anim;

    private void Awake()
    {
        state = PlayerState.Idle;
        anim = GetComponent<Animator>();
    }

    public void SetState(PlayerState newState)
    {
        if (state == newState)
            return;
        state = newState;
        if(state == PlayerState.Walk)
            anim.SetTrigger("Walk");
        else if(state == PlayerState.Run)
            anim.SetTrigger("Run");
        else if(state == PlayerState.Jump)
            anim.SetTrigger("Jump");
        else if(state == PlayerState.Attack)
            anim.SetTrigger("Attack");
        else if(state == PlayerState.Dead)
            anim.SetTrigger("Dead");
        else if(state == PlayerState.Climb)
            anim.SetTrigger("Climb");
        else
            anim.SetTrigger("Idle");
    }
}
