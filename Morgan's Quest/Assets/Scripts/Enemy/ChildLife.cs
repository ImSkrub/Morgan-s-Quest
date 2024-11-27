using ABB_EnemyPriority;
using System;
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
    [SerializeField] private float destroyDelay = 0.5f;
    [SerializeField] private GenerateItem item;

    private ABB enemyTree; 
    private Transform player;
    private float lastDistance;
    public event Action OnDeath;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        item = GetComponent<GenerateItem>();

        
        enemyTree = new ABB();
        enemyTree.InicializarArbol();

       
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            
            lastDistance = Vector2.Distance(transform.position, player.position);
            enemyTree.AgregarElem(gameObject.name, lastDistance);
        }
        else
        {
            Debug.LogError("Player not found. Ensure it has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (!isDead && player != null)
        {
            
            float currentDistance = Vector2.Distance(transform.position, player.position);

            if (Mathf.Abs(currentDistance - lastDistance) > 0.1f)
            {
                
                enemyTree.EliminarElem(gameObject.name);  
                enemyTree.AgregarElem(gameObject.name, currentDistance);  
                lastDistance = currentDistance;
            }

            
            if (currentDistance < 1.5f)
            {
                AttackClosestEnemy();
            }
        }
    }

    private void AttackClosestEnemy()
    {
        string closestEnemy = enemyTree.EnemigoMasCercano();
       
        Debug.Log($"Attacking closest enemy: {closestEnemy}");
    }

    public void GetDamage(int value)
    {
        if (isDead) return;

        health -= value;

        if (health <= 0f && !isDead)
        {
            Die();
        }
        else
        {
            spriteRenderer.color = damageColor;
            Invoke("RestoreColor", 0.5f);
        }
    }

    private void RestoreColor()
    {
        if (!isDead)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void Die()
    {
        isDead = true;

        // Obtener la instancia de QuickSortHS para agregar puntajes
        QuickSortHS quickSortHS = FindObjectOfType<QuickSortHS>();
        if (quickSortHS != null)
        {
            quickSortHS.AgregarPuntaje(10); // Solo agregar el puntaje cuando el enemigo muere
            Debug.Log("Puntaje agregado al morir el enemigo.");
        }
        else
        {
            Debug.LogWarning("QuickSortHS no encontrado.");
        }

        // Eliminar el enemigo de la lista de enemigos
        enemyTree.EliminarElem(gameObject.name);

        // Llamar al evento de muerte y destruir el objeto después de un pequeño retraso
        OnDeath?.Invoke();
        Destroy(gameObject, destroyDelay);

        // Generar ítem al morir el enemigo
        item.SpawnItem();

        // Incrementar el contador de enemigos muertos en el GameManager
        GameManager.Instance.counter += 1;
    }

    public string GetClosestEnemy()
    {
        return enemyTree.EnemigoMasCercano();
    }

    public string GetFarthestEnemy()
    {
        return enemyTree.EnemigoMasLejano();
    }
}
