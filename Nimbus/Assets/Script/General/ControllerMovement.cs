using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{
    [SerializeField] protected float speedWalk = 5f;
    protected Rigidbody2D rb;
    protected ControllerState controllerAnimState;
    protected float speed, speedRun;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speedRun = speedWalk * 2;
        controllerAnimState = GetComponent<ControllerState>();
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    public void Disable()
    {
        Stop();
        this.enabled = false;
    }

    protected virtual void Flip(bool value)
    {
        if (value)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (!value)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
