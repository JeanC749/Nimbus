using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : ControllerHealth
{
    protected override IEnumerator Die()
    {
        controllerMovement.Disable();
        controllerAnimaState.SetState(AnimationStates.Dead);
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }

    protected override IEnumerator Hurt()
    {
        controllerMovement.Stop();
        controllerMovement.enabled = false;
        controllerAnimaState.SetState(AnimationStates.Hurt);
        yield return new WaitForSeconds(0.3f);
        controllerAnimaState.SetState(AnimationStates.Idle);
    }
}
