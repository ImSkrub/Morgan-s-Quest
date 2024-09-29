using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLife : MonoBehaviour
{
    public float health = 25f;
    private bool isDead = false; 
    //Animator anim;
    public float delay = 2f; 
    public float destroyDelay = 0.5f; 

    //Color al recibir da�o.
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color damageColor = Color.red;
    private Color originalColor;

    //Drop de escencia
    private GenerateItem item;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        item = GetComponent<GenerateItem>();
    }

    private void Update()
    {
        if (health <= 0f && !isDead )
        {
            Die();
        }
    }

    //Recibir da�o
    public void GetDamage(int value)
    {
        health -= value; //currentHealth = currentHealth - value; 
        spriteRenderer.color = damageColor;
        Invoke("RestoreColor", 0.5f);
    }

    private void RestoreColor()
    {
        // Restaurar el color original del sprite
        spriteRenderer.color = originalColor;
    }
    public void Die()
    {
        isDead = true;
        //anim.SetTrigger("Death");
        Destroy(gameObject, destroyDelay);
        item.SpawnItem();
        GameManager.Instance.counter += 1;
    }
}