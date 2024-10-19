using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f, jumpForce = 450f, wallSlideSpeed = 2f;
    [SerializeField] private LayerMask groundLayer, wallLayer;
    [SerializeField] private Transform groundCheck, wallCheck;
    private Rigidbody2D rb;
    private float inputHor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputHor = Input.GetAxisRaw("Horizontal");
        Flip();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            Jump();
    }

    private bool IsTouchingWall()
    {
        Ray2D ray = new(wallCheck.position, Vector2.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.2f, wallLayer);
        return hit.collider != null;
    }

    private void Climb()
    {
        if (Input.GetKey(KeyCode.E))
        {
            float inputVer = Input.GetAxisRaw("Vertical");
            Debug.Log($"Input Vertical: {inputVer}");
            rb.velocity = new Vector2(rb.velocity.x, inputVer * speed);
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
        if (inputHor > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (inputHor < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * (jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        Ray2D ray = new(groundCheck.position, Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.05f, groundLayer);
        return hit.collider != null;
    }

    private void Movement()
    {
        rb.velocity = new Vector2(inputHor * speed, rb.velocity.y);
    }
}
