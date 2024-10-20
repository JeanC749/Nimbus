using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullaje : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocidad;
    public LayerMask capaAbajo;
    public LayerMask capaEnfrente;
    public float distanciaEnfrente;
    public float distanciaAbajo;
    public Transform controladorEnfrente;
    public Transform controladorAbajo;
    public bool informacionAbajo;
    public bool informacionEnfrente;
    private bool mirandoAlaDerecha = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Asigna las capas usando LayerMask.NameToLayer
        capaAbajo = LayerMask.GetMask("Suelo");
        capaEnfrente = LayerMask.GetMask("Pared");
    }

    // Update is called once per frame
    private void Update()
    {
        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        informacionEnfrente = Physics2D.Raycast(controladorEnfrente.position, transform.right, distanciaEnfrente, capaEnfrente);
        informacionAbajo = Physics2D.Raycast(controladorAbajo.position, transform.up * -1, distanciaAbajo, capaAbajo);
        if (informacionEnfrente || !informacionAbajo)
        {
            Girar();
        }
    }
    private void Girar()
    {
        mirandoAlaDerecha = !mirandoAlaDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

}
