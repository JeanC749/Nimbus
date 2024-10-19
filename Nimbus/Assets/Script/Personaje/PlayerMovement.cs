using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody2D rb;
    private float inputHor;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        inputHor = Input.GetAxisRaw("Horizontal");
        Flip();
        if(Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate() {
        Movimiento();
    }

    private void Flip() {
        if (inputHor > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (inputHor < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    
    private void Jump() {
        Debug.Log("Jump");
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * (jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
    } 

    private void Movimiento() {
        rb.velocity = new Vector2(inputHor * speed, rb.velocity.y);
    }
}
