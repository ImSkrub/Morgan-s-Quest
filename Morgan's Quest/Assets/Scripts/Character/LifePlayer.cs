using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LifePlayer : MonoBehaviour, IDamageable
{
    public event Action OnDeath; // Asegúrate de que este evento esté definido

    // Variables de vida
    public float currentHealth { get; private set; } // Implementa la propiedad CurrentHealth
    public int maxHealth;

    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    private Color originalColor;

    // Inmortalidad y escudo
    public int inmortal = 0;

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
                GameManager.Instance.escence -= essence.value;
            }
        }
    }

    private void RestoreColor()
    {
        spriteRenderer.color = originalColor;
    }

    public void Die()
    {
        Destroy(gameObject, 1f);
        OnDeath?.Invoke(); // Llama al evento OnDeath

        // Reiniciar la pila de essences
        EssenceManager.instance.escenceStack.InitializeStack();
        GameManager.Instance.escence = 0;
    }
}