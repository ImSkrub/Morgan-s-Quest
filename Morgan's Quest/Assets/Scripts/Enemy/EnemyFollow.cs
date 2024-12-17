using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    public GraphController graphController;  // Referencia al GraphController
    public Transform jugador;  // Referencia al jugador
    public float velocidad = 3f;  // Velocidad de movimiento del enemigo
    public float distanciaDeteccion = 5f;  // Distancia de detecci�n del jugador
    public float rayDistance = 1f;

    private List<Waypoint> camino;  // Lista de waypoints por donde el enemigo pasar�
    private int waypointIndex = 0;  // �ndice del waypoint actual

    private IObstacleAvoidance obstacleAvoidance;

    private void Awake()
    {
        graphController = FindAnyObjectByType<GraphController>();
        
        if (graphController == null)
        {
            Debug.LogError("GraphController no est� asignado.");
            return;
        }
        
        jugador = FindObjectOfType<Player>().transform;

        obstacleAvoidance = new ObstacleAvoidance(LayerMask.GetMask("Wall"));
    }

    void Start()
    {
        base.Start();
        ActualizarCamino();
    }

    void Update()
    {
        base.Update();

        if (camino.Count == 0) return;

        MoverHaciaWaypoint();

        if (waypointIndex < camino.Count && Vector3.Distance(transform.position, camino[waypointIndex].transform.position) < 0.5f)
        {
            waypointIndex++;
            if (waypointIndex >= camino.Count)
            {
                ActualizarCamino();
            }
        }
    }

    // Obtener el waypoint m�s cercano a una posici�n dada
    private Waypoint ObtenerWaypointMasCercano(Vector3 posicion)
    {
        Waypoint closestWaypoint = null;
        float minDistancia = Mathf.Infinity;

        foreach (Waypoint waypoint in graphController.nodos)
        {
            float distancia = Vector3.Distance(posicion, waypoint.transform.position);
            if (distancia < minDistancia)
            {
                minDistancia = distancia;
                closestWaypoint = waypoint;
            }
        }

        return closestWaypoint;
    }

    // Recalcular el camino hacia el jugador
    private void ActualizarCamino()
    {
        // Obtener el waypoint m�s cercano al enemigo y al jugador
        Waypoint waypointInicio = ObtenerWaypointMasCercano(transform.position);
        Waypoint waypointDestino = ObtenerWaypointMasCercano(jugador.position);

        // Obtener el camino m�s corto usando Dijkstra
        Pathfinding pathfinding = new Pathfinding(graphController);
        camino = pathfinding.Dijkstra(waypointInicio, waypointDestino);

        waypointIndex = 0;  // Reiniciar el �ndice de waypoint
    }

    // Mover al enemigo hacia el waypoint actual
    private void MoverHaciaWaypoint()
    {
        if (waypointIndex < camino.Count)
        {
            Vector3 targetDirection = (camino[waypointIndex].transform.position - transform.position).normalized;

            // Ajusta la direcci�n usando la l�gica de evasi�n
            Vector3 adjustedDirection = obstacleAvoidance.GetAdjustedDirection(transform, targetDirection, rayDistance);

            // Aplica el movimiento
            transform.position += adjustedDirection * velocidad * Time.deltaTime;
        }
    }

}