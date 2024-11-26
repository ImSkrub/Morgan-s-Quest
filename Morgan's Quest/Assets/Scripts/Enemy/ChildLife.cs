using ABB_EnemyPriority;
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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        item = GetComponent<GenerateItem>();

        // Buscar referencias necesarias
        enemyTree = FindObjectOfType<GameManager>().enemyTree;  // Obtén el ABB desde el GameManager
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null)
        {
            
            lastDistance = Vector2.Distance(transform.position, player.position);
            enemyTree.AgregarElem(gameObject.name, lastDistance); 
        }
        else
        {
            Debug.LogError("Jugador no encontrado. Asegúrate de que tenga la etiqueta 'Player'.");
        }
    }

    private void Update()
    {
        if (!isDead && player != null)
        {
            // Calcular distancia actual al jugador
            float currentDistance = Vector2.Distance(transform.position, player.position);

            if (Mathf.Abs(currentDistance - lastDistance) > 0.1f)
            {
                // Actualizar posición en el ABB
                enemyTree.EliminarElem(gameObject.name);  // Eliminar por nombre del enemigo
                enemyTree.AgregarElem(gameObject.name, currentDistance);  // Agregar por nombre y distancia
                lastDistance = currentDistance;
            }
        }
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

        
        item.SpawnItem();  

        
        GameManager.Instance.counter += 1;

       
        Destroy(gameObject, destroyDelay);
    }
}