using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 7f, invincibilityDuration = 2f, blinkInterval = 0.1f;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(float damage, Collider2D player, Collider2D enemy)
    {
        health -= damage;
        if (health > 0)
            StartCoroutine(Invencibility(player, enemy));
        else
            Destroy(gameObject);
    }

    private IEnumerator Invencibility(Collider2D player, Collider2D enemy)
    {
        // Ignorar colisiones con el enemigo
        Physics2D.IgnoreCollision(player, enemy, true);
        playerMovement.Stop();
        playerMovement.enabled = false;
        yield return new WaitForSeconds(1f);
        playerMovement.enabled = true;
        float elapsedTime = 0f;
        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Alterna visibilidad
            yield return new WaitForSeconds(blinkInterval); // Espera un intervalo
            elapsedTime += blinkInterval;
        }
        spriteRenderer.enabled = true;
        Physics2D.IgnoreCollision(player, enemy, false);
    }
}
