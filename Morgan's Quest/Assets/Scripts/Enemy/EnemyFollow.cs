using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    
    [SerializeField] private float followSpeed = 25f;    // Velocidad de seguimiento
    [SerializeField] private float stopDistance = 1f;   // Distancia mínima antes de detenerse
    [SerializeField] private float detectionRadius = 90f;  // Radio de detección

    private Transform player;  // Referencia al jugador
    private Rigidbody2D rb;    // Rigidbody2D del enemigo

    private void Start()
    {
        // Buscar al jugador por la etiqueta "Player"
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            Debug.LogError("No se encontró el jugador. Asegúrate de que el jugador tenga la etiqueta 'Player'.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Calcula la distancia al jugador
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRadius && distanceToPlayer > stopDistance)
            {
                // Moverse hacia el jugador
                Vector2 direction = (player.position - transform.position).normalized;
                rb.MovePosition((Vector2)transform.position + direction * followSpeed * Time.deltaTime);
            }
        }
    }
}
