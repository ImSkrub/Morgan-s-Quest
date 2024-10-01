using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChildLife : MonoBehaviour
{
    public float health = 25f;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float destroyDelay = 0.5f;  // Retardo antes de destruir el enemigo
    [SerializeField] private GenerateItem item;  // Referencia al script GenerateItem para dropear la esencia

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        item = GetComponent<GenerateItem>();
    }

    public void GetDamage(int value)
    {
        if (isDead) return;  // Si ya está muerto, no hacer nada

        health -= value;  // Reducir la vida

        if (health <= 0f && !isDead)
        {
            Die();  // Si la vida llega a 0 o menos, morir
        }
        else
        {
            spriteRenderer.color = damageColor;  // Cambiar color al recibir daño
            Invoke("RestoreColor", 0.5f);  // Restaurar color original después de un tiempo
        }
    }

    private void RestoreColor()
    {
        if (!isDead)
        {
            spriteRenderer.color = originalColor;  // Restaurar el color original del sprite
        }
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject, destroyDelay);  // Destruir el objeto después del retardo
        item.SpawnItem();  // Generar el ítem al morir

        GameManager.Instance.counter += 1;  // Incrementar el contador de enemigos muertos en el GameManager
    }
}
