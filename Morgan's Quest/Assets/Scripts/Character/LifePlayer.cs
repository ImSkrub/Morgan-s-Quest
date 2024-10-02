using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LifePlayer : MonoBehaviour, IDamageable
{
    // Variables de vida
    public float currentHealth;
    public int maxHealth;

    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    private Color originalColor;

    // Inmortalidad y escudo
    public int inmortal = 0;

    // Evento de muerte
    public event Action OnDeath;
    public Image barraHp;

    // Implementación de la propiedad requerida por IDamageable
    public float CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
        barraHp.fillAmount = currentHealth/maxHealth;
    }

    public void GetDamage(int value)
    {
        if (inmortal == 0)
        {
            currentHealth -= value;
            spriteRenderer.color = damageColor;
            Invoke("RestoreColor", 0.5f);

            if (!EssenceManager.instance.escenceStack.IsEmpty())
            {
                Essence essence = EssenceManager.instance.escenceStack.Pop();
                GameManager.Instance.escence -= essence.value; // Asegúrate que 'value' sea accesible
            }
        }
    }

    private void RestoreColor()
    {
        spriteRenderer.color = originalColor;
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        OnDeath?.Invoke(); // Invocamos el evento de muerte si está suscrito

        // Reiniciar la pila de essences
        EssenceManager.instance.escenceStack.InitializeStack();
        GameManager.Instance.escence = 0;
        GameManager.Instance.counter = 0;

    }
}
