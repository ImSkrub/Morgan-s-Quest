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

    private ABB enemyTree; // Instance of ABB
    private Transform player;
    private float lastDistance;
    public event Action OnDeath;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        item = GetComponent<GenerateItem>();

        // Initialize the ABB
        enemyTree = new ABB();
        enemyTree.InicializarArbol();

        // Find reference to the player
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            // Calculate initial distance and add this enemy to the ABB
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
            // Calculate current distance to the player
            float currentDistance = Vector2.Distance(transform.position, player.position);

            if (Mathf.Abs(currentDistance - lastDistance) > 0.1f)
            {
                // Update position in the ABB
                enemyTree.EliminarElem(gameObject.name);  // Remove by enemy name
                enemyTree.AgregarElem(gameObject.name, currentDistance);  // Add by name and distance
                lastDistance = currentDistance;
            }

            // Check if the enemy can attack
            if (currentDistance < 1.5f)
            {
                AttackClosestEnemy();
            }
        }
    }

    private void AttackClosestEnemy()
    {
        string closestEnemy = enemyTree.EnemigoMasCercano();
        // Implement attack logic here, e.g., call a method to deal damage to the closest enemy
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

        enemyTree.EliminarElem(gameObject.name);
        OnDeath?.Invoke();
        Destroy(gameObject, destroyDelay);
        item.SpawnItem();

        GameManager.Instance.counter += 1;
    }

    // Additional methods for querying the ABB
    public string GetClosestEnemy()
    {
        return enemyTree.EnemigoMasCercano();
    }

    public string GetFarthestEnemy()
    {
        return enemyTree.EnemigoMasLejano();
    }
}
