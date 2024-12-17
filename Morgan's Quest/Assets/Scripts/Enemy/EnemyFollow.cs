using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    public GraphController graphController;  // Referencia al GraphController
    public Transform jugador;  // Referencia al jugador
    public float velocidad = 3f;  // Velocidad de movimiento del enemigo
    public float distanciaDeteccion = 5f;  // Distancia de detección del jugador

    private List<Waypoint> camino;  // Lista de waypoints por donde el enemigo pasará
    private int waypointIndex = 0;  // Índice del waypoint actual

    private void Awake()
    {
        graphController = FindAnyObjectByType<GraphController>();
        jugador = FindObjectOfType<Player>().transform;
    }

    void Start()
    {
        base.Start();

        if (graphController == null)
        {
            Debug.LogError("GraphController no está asignado.");
            return;
        }

        // Inicializa el camino al comienzo
        ActualizarCamino();
    }

    void Update()
    {
        base.Update();

        

        if (camino.Count == 0) return;  // Si no hay camino, no hacer nada

        // Mover al enemigo hacia el waypoint más cercano
        MoverHaciaWaypoint();

        // Verificar si el enemigo ha llegado al waypoint
        if (waypointIndex < camino.Count && Vector3.Distance(transform.position, camino[waypointIndex].transform.position) < 0.5f)
        {
            waypointIndex++;  // Avanzar al siguiente waypoint
            if (waypointIndex >= camino.Count)
            {
                // Recalcular el camino en cada frame
                ActualizarCamino();
                Debug.Log("Enemigo ha llegado al destino.");
            }
        }
    }

    // Obtener el waypoint más cercano a una posición dada
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
        // Obtener el waypoint más cercano al enemigo y al jugador
        Waypoint waypointInicio = ObtenerWaypointMasCercano(transform.position);
        Waypoint waypointDestino = ObtenerWaypointMasCercano(jugador.position);

        // Obtener el camino más corto usando Dijkstra
        Pathfinding pathfinding = new Pathfinding(graphController);
        camino = pathfinding.Dijkstra(waypointInicio, waypointDestino);

        waypointIndex = 0;  // Reiniciar el índice de waypoint
    }

    // Mover al enemigo hacia el waypoint actual
    private void MoverHaciaWaypoint()
    {
        if (waypointIndex < camino.Count)
        {
            Vector3 direccion = (camino[waypointIndex].transform.position - transform.position).normalized;
            transform.position += direccion * velocidad * Time.deltaTime;
        }
    }
}
