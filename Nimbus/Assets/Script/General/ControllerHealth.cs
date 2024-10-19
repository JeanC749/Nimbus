using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHealth : MonoBehaviour
{
    [SerializeField] protected float health = 7f, invincibilityDuration = 2f, blinkInterval = 0.1f;
    protected SpriteRenderer spriteRenderer;
    protected ControllerMovement controllerMovement;
    protected ControllerState controllerAnimaState;
    protected bool isInvincible = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controllerMovement = GetComponent<PlayerMovement>();
        controllerAnimaState = GetComponent<ControllerPlayerState>();
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
            return;
        health -= damage;
        if (health > 0)
            StartCoroutine(Invencibility());
        else
            StartCoroutine(Die());
    }

    protected virtual IEnumerator Die()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }

    protected virtual IEnumerator Invencibility()
    {
        isInvincible = true;
        yield return StartCoroutine(Hurt());
        controllerMovement.enabled = true;
        float elapsedTime = 0f;
        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Alterna visibilidad
            yield return new WaitForSeconds(blinkInterval); // Espera un intervalo
            elapsedTime += blinkInterval;
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    protected virtual IEnumerator Hurt()
    {
        controllerMovement.Stop();
        controllerMovement.enabled = false;
        controllerAnimaState.SetState(AnimationStates.Hurt);
        yield return new WaitForSeconds(0.5f);
    }

}
