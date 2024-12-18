using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveArea : MonoBehaviour
{
    private EnemyFollow enemy;
    private bool playerInRange = false; // Bandera para saber si el jugador está dentro del área

    private void Awake()
    {
        enemy = GetComponent<EnemyFollow>();

    }

    // Usamos un Collider2D como área de activación
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // El jugador ha entrado en el área
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // El jugador ha salido del área
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            MoveTowardsPlayer(); // Moverse hacia el jugador
        }
    }

    private void MoveTowardsPlayer()
    {
        
        Vector3 direction = (enemy.jugador.position - transform.position).normalized;
                
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, enemy.rayDistance, enemy.wallLayer);

        if (hit.collider != null)
        {
           
            Vector3 normal = hit.normal; 
            direction = Vector3.Reflect(direction, normal); 
        }

        // Movimiento del enemigo
        transform.position += direction * enemy.velocidad * Time.deltaTime;
    }
}
