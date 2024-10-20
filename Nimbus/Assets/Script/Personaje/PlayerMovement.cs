using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : ControllerMovement
{
    [SerializeField] private float jumpForce = 12f, wallSlideSpeed = 2f, stamina = 100f,radioGolpe = 5f;
    [SerializeField] private Vector2 boxSize = new(0.5f, 0.1f);
    [SerializeField] private LayerMask groundLayer, wallLayer;
    [SerializeField] private Transform groundCheck, wallCheck, attackArea;
    private float inputHor, inputVer;
    private void Update()
    {
        inputHor = Input.GetAxisRaw("Horizontal");
        inputVer = Input.GetAxisRaw("Vertical");
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.E))
            SetStamina(0.5f);
        if (inputHor > 0)
            Flip(true);
        else if (inputHor < 0)
            Flip(false);
        Run();
        if (Input.GetMouseButtonDown(0))
            StartAttack();
    }

    private void SetStamina(float value)
    {
        stamina += value;
        if (stamina > 100)
            stamina = 100;
        else if (stamina < 0)
            stamina = 0;
    }
    private bool IsTouchingWall()
    {
        // Detectar si estamos tocando una pared en función de la dirección del personaje
        Vector2 direction = Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, direction, 0.1f, wallLayer);
        return hit.collider != null;
    }

    private void Climb()
    {
        if (Input.GetKey(KeyCode.E) && stamina > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, inputVer * speed);
            SetStamina(-0.5f);
        }
        else
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
    }

    private void StartAttack()
    {
        // Si no estamos tocando el suelo, cancelamos el ataque.
        if (!IsGrounded()) return;
        controllerAnimState.SetState(AnimationStates.Attack);
    }

    private void Attack()
    {
        // Detecta los objetos dentro del área de ataque
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackArea.position, radioGolpe);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemigo"))
            {
                collider.GetComponent<ControllerHealth>().TakeDamage(1);
            }
        }
    }

    private void FinishAttack()
    {
        controllerAnimState.SetState(AnimationStates.Idle);
    }

    private void FixedUpdate()
    {
        if (controllerAnimState.States == AnimationStates.Attack)
        {
            Stop();
            return;
        }
        ManagerState();
        Movement();
        if (Input.GetKey(KeyCode.Space) && IsGrounded() && !Input.GetKey(KeyCode.E))
            Jump();
        if (IsTouchingWall())
            Climb();
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        // Comprobar si el cuadrado toca alguna capa de suelo o pared
        bool isTouchingGround = Physics2D.OverlapBox(groundCheck.position, boxSize, 0f, groundLayer);
        bool isTouchingWall = Physics2D.OverlapBox(groundCheck.position, boxSize, 0f, wallLayer);
        return isTouchingGround || isTouchingWall;
    }

    private void Run()
    {
        if (Math.Abs(inputHor) > 0 && IsGrounded() && Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            speed = speedRun;
            SetStamina(-0.5f);
        }
        else
            speed = speedWalk;
    }


    private void ManagerState()
    {
        if (IsGrounded())
        {
            if (Input.GetKey(KeyCode.Space))
                controllerAnimState.SetState(AnimationStates.Jump);
            else if (speed == speedRun)
                controllerAnimState.SetState(AnimationStates.Run);
            else if (inputHor != 0 && inputVer == 0)
                controllerAnimState.SetState(AnimationStates.Walk);
            else if (inputHor == 0 && inputVer == 0)
                controllerAnimState.SetState(AnimationStates.Idle);
        }
        else if (!IsGrounded() && !IsTouchingWall() && rb.velocity.y < 0)
            controllerAnimState.SetState(AnimationStates.Fall);
        else if (IsTouchingWall())
            controllerAnimState.SetState(AnimationStates.Climb);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(inputHor * speed, rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackArea.position, radioGolpe);
    }
}
