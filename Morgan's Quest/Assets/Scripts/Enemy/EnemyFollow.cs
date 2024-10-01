using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    
    [SerializeField] private float detectionRadius = 90f;  // Radio de detección

    private void Start()
    {
        
        // Buscar al jugador por la etiqueta "Player"
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

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

            if (distanceToPlayer < detectionRadius && distanceToPlayer > closestDist)
            {
                // Moverse hacia el jugador
                Vector2 direction = (player.position - transform.position).normalized;
                rb.MovePosition((Vector2)transform.position + direction * vel * Time.deltaTime);
            }
        }
    }
}
