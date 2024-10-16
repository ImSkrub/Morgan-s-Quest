using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    private void Start()
    {
        
        // Buscar al jugador por la etiqueta "Player"
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("No se encontr� el jugador. Aseg�rate de que el jugador tenga la etiqueta 'Player'.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Calcula la distancia al jugador
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < distToAttack && distanceToPlayer > closestDist)
            {
                // Moverse hacia el jugador
                Vector2 direction = (player.position - transform.position).normalized;
                rb.MovePosition((Vector2)transform.position + direction * vel * Time.deltaTime);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<LifePlayer>().GetDamage(damage);
        }
    }
}
