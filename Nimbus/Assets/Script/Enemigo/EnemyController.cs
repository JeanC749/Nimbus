using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public float patrolSpeed = 2.0f;  // Velocidad de patrullaje
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private Transform ControladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool MovimientoDerecha = true;
    private bool isFollowingPlayer = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = patrolSpeed;  // Inicia con la velocidad de patrullaje
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
            // El enemigo patrulla de izquierda a derecha
            isFollowingPlayer = false;
            movement = new Vector2(MovimientoDerecha ? 1 : -1, 0);  // Se mueve dependiendo de la dirección
        }

        // Comprobamos si hay suelo delante
        RaycastHit2D informacionSuelo = Physics2D.Raycast(ControladorSuelo.position, Vector2.down, distancia, groundLayer);
        if (informacionSuelo.collider == null && !isFollowingPlayer)
        {
            Girar();  // Gira si no detecta suelo y no está siguiendo al jugador
        }

        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);  // Aplicamos la velocidad de movimiento
    }

    private void Girar()
    {
        MovimientoDerecha = !MovimientoDerecha;
        transform.eulerAngles = new Vector3(0, MovimientoDerecha ? 0 : 180, 0);  // Gira el sprite
    }
}

