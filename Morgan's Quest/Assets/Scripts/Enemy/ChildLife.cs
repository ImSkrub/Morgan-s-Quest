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

    private ABB enemyTree; // Instancia de ABB
    private Transform player;
    private float lastDistance;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        item = GetComponent<GenerateItem>();

        // Inicializar el ABB
        enemyTree = new ABB();
        enemyTree.InicializarArbol();

        // Buscar referencia al jugador
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            // Calcular la distancia inicial y agregar este enemigo al ABB
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

        // Eliminar del ABB al morir
        enemyTree.EliminarElem(gameObject.name);

        // Destruir el objeto después del retardo
        Destroy(gameObject, destroyDelay);
        item.SpawnItem();

        // Incrementar el contador global si es necesario
        GameManager.Instance.counter += 1;
    }

    // Métodos adicionales para consultar el ABB:
    public string GetClosestEnemy()
    {
        return enemyTree.EnemigoMasCercano();
    }

    public string GetFarthestEnemy()
    {
        return enemyTree.EnemigoMasLejano();
    }
}
