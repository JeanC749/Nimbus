using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float velocidadMovimiento;

    private int siguientePlataforma = 1;
    private bool ordenPlataformas = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ordenPlataformas && siguientePlataforma + 1 >= puntosMovimiento.Length)
        {
            ordenPlataformas = false;
        }

        if (!ordenPlataformas && siguientePlataforma <= 0)
        {
            ordenPlataformas = true;
        }

        if (siguientePlataforma < puntosMovimiento.Length && Vector2.Distance(transform.position, puntosMovimiento[siguientePlataforma].position) < 0.1f)
        {
            if (ordenPlataformas)
            {
                siguientePlataforma += 1;
            }
            else
            {
                siguientePlataforma -= 1;
            }
        }

        if (siguientePlataforma < puntosMovimiento.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePlataforma].position,
            velocidadMovimiento * Time.deltaTime);
        }
        
    }
}
