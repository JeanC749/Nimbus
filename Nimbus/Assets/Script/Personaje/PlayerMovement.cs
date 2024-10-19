using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f, jumpForce = 12f, wallSlideSpeed = 2f,stamina = 100f;
    [SerializeField] private LayerMask groundLayer, wallLayer;
    [SerializeField] private Transform groundCheck, wallCheck;
    
    private Rigidbody2D rb;
    private float inputHor,speedRun;
    private bool facingRight = true; // Saber si el personaje está mirando a la derecha

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speedRun = speed * 2;
    }

    public void Stop(){
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        inputHor = Input.GetAxisRaw("Horizontal");
        if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.E))
            SetStamina(0.5f);
        Flip();
        Run();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !Input.GetKey(KeyCode.E))
            Jump();
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
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, direction, 0.2f, wallLayer);
        return hit.collider != null;
    }

    private void Climb()
    {
        if (Input.GetKey(KeyCode.E) && stamina > 0)
        {
            float inputVer = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, inputVer * speed);
            SetStamina(-0.5f);
        }
        else
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
    }

    private void FixedUpdate()
    {
        Movement();
        if (IsTouchingWall())
            Climb();
    }

    private void Flip()
    {
        if (inputHor > 0 && !facingRight)
        {
            facingRight = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (inputHor < 0 && facingRight)
        {
            facingRight = false;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            speed = speedRun;
            SetStamina(-0.5f);
        }
        else
            speed = 5f;
    }

    private void Movement()
    {
        rb.velocity = new Vector2(inputHor * speed, rb.velocity.y);
    }
}
