using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isFollowingPlayer = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        if (distanceToPlayer < detectionRadius)
        {
            // El enemigo sigue al jugador
            isFollowingPlayer = true;
            Vector2 direccion = (Player.position - transform.position).normalized;
            movement = new Vector2(direccion.x, 0);  // Solo se mueve en el eje X
        }
        else
        {
            // El enemigo deja de seguir al jugador si está fuera del radio de detección
            isFollowingPlayer = false;
            movement = Vector2.zero;  // No se mueve si no está siguiendo al jugador
        }

        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);  // Aplicamos la velocidad de movimiento solo cuando sigue al jugador
    }
}