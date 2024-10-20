using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : ControllerHealth
{
    [SerializeField] private CameraEffects cameraEffects;
    protected override IEnumerator Die()
    {
        controllerMovement.Disable();
        controllerAnimaState.SetState(AnimationStates.Dead);
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }

    protected override IEnumerator Hurt()
    {
        StartCoroutine(cameraEffects.Snake(0.4f, 1f));
        controllerMovement.Stop();
        controllerMovement.enabled = false;
        controllerAnimaState.SetState(AnimationStates.Hurt);
        yield return new WaitForSeconds(0.4f);
    }
}
