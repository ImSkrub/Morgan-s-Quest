using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLife : MonoBehaviour
{
    public float health = 25f;
    private bool isDead = false;
    //Animator anim;
    public float destroyDelay = 0.5f;  // Retardo antes de destruir el enemigo
    public int currentLife = 25; 
    // Color al recibir daño
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color damageColor = Color.red;
    private Color originalColor;

    // Drop de esencia
    private GenerateItem item;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        item = GetComponent<GenerateItem>();
    }

    // Recibir daño
    public void GetDamage(int value)
    {
        if (isDead) return;  // Si ya está muerto, no hacer nada

        health -= value; // Reducir la vida

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
        // Restaurar el color original del sprite si el enemigo no ha muerto
        if (!isDead)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void Die()
    {
        isDead = true;
        //anim.SetTrigger("Death");  // Animación de muerte si existe
        Destroy(gameObject, destroyDelay);  // Destruir el objeto después del retardo
        item.SpawnItem();  // Generar el ítem al morir
        GameManager.Instance.counter += 1;  // Incrementar el contador de enemigos muertos en el GameManager
    }
}