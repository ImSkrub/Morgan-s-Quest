using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveArea : MonoBehaviour
{
    private EnemyFollow enemy;
    private bool playerInRange = false; // Bandera para saber si el jugador est� dentro del �rea

    private void Awake()
    {
        enemy = GetComponent<EnemyFollow>();

    }

    // Usamos un Collider2D como �rea de activaci�n
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // El jugador ha entrado en el �rea
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // El jugador ha salido del �rea
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
        // Mover al enemigo hacia el jugador
        Vector3 direction = (enemy.jugador.position - transform.position).normalized; // Direcci�n hacia el jugador
        transform.position += direction * enemy.velocidad * Time.deltaTime; // Movimiento hacia el jugador
    }
}
