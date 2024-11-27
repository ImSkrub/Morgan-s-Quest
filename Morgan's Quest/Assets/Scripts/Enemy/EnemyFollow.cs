using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFollow : Enemy
{
    private List<Vertice> path = new List<Vertice>();
    private int currentWaypointIndex = 0;
    public float speed = 2;

    private GraphManager graphManager; // Almacenar referencia a GraphManager
    private float pathUpdateThreshold = 0.5f; // Distancia mínima para actualizar el camino

    private void Start()
    {
        base.Start();
        graphManager = FindObjectOfType<GraphManager>(); // Obtener referencia a GraphManager una vez
    }

    private void Update()
    {
        base.Update();

        if (player != null) // Asegurarse de que el jugador no sea nulo
        {
            // Obtener el waypoint específico del jugador
            Vertice playerWaypoint = graphManager.GetSpecificWaypoint(player.gameObject);
            if (playerWaypoint != null)
            {
                // Moverse hacia el waypoint si está fuera del rango de ataque
                if (Vector2.Distance(transform.position, player.position) >= distToAttack)
                {
                    MoveTowardsWaypoint(playerWaypoint);
                }
            }
            else
            {
                Debug.LogWarning("No se encontró el waypoint del jugador.");
            }
        }
        else
        {
            Debug.LogWarning("Player reference is null!");
        }
    }

    private void MoveTowardsWaypoint(Vertice targetWaypoint)
    {
        Vector2 targetPosition = targetWaypoint.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Opcional: Si quieres que el enemigo haga algo al llegar al waypoint
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            Debug.Log("Reached target waypoint!");
            // Aquí puedes agregar lógica adicional si es necesario
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja Gizmos para visualizar el camino hacia el waypoint
        if (player != null)
        {
            Vertice playerWaypoint = graphManager.GetSpecificWaypoint(player.gameObject);
            if (playerWaypoint != null)
            {
                Gizmos.color = Color.green; // Color para el waypoint
                Gizmos.DrawSphere(playerWaypoint.transform.position, 0.2f); // Dibuja un círculo en el waypoint del jugador
            }
        }
    }
}
