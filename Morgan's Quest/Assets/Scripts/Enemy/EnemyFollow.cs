using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    public GraphController graphController;
    public Transform jugador;
    public float velocidad = 3f;
    public float rayDistance = 1f;
    public float tiempoRecalculo = 1f;

    private Vector3 ultimaPosicionJugador;

    public LayerMask wallLayer;
    public List<Waypoint> camino;
    private int waypointIndex = 0;
    private float tiempoUltimoRecalculo;

    private IObstacleAvoidance obstacleAvoidance;
    public enum NavigationMode { Dijkstra, Waypoints }
    public NavigationMode modoNavegacion = NavigationMode.Dijkstra;

    private void Awake()
    {
        graphController = FindAnyObjectByType<GraphController>();

        if (graphController == null)
        {
            Debug.LogError("GraphController no est� asignado.");
            return;
        }

        jugador = FindObjectOfType<Player>().transform;

        obstacleAvoidance = new ObstacleAvoidance(wallLayer, graphController);
    }

    void Start()
    {
        base.Start();
        ActualizarCamino();
        ultimaPosicionJugador = jugador.position;
        tiempoUltimoRecalculo = Time.time;
    }

    private void Update()
    {
        base.Update();

        if (camino == null || camino.Count == 0) return;

        RecalculatePathIfNeeded();

        if (waypointIndex < camino.Count)
        {
            MoverHaciaWaypoint(camino[waypointIndex]);
        }
    }

    private void RecalculatePathIfNeeded()
    {
        bool jugadorSeMovio = Vector3.Distance(jugador.position, ultimaPosicionJugador) > 1f;
        bool tiempoExcedido = Time.time - tiempoUltimoRecalculo > tiempoRecalculo;

        if (jugadorSeMovio || tiempoExcedido || waypointIndex >= camino.Count)
        {
            ActualizarCamino();
            ultimaPosicionJugador = jugador.position;
            tiempoUltimoRecalculo = Time.time;
        }
    }


    private void ActualizarCamino()
    {
        Waypoint inicio = ObtenerWaypointMasCercano(transform.position);
        Waypoint destino = ObtenerWaypointMasCercano(jugador.position);

        Pathfinding pathfinding = new Pathfinding(graphController);
        camino = pathfinding.Dijkstra(inicio, destino, wallLayer);

        waypointIndex = 0;

        if (camino.Count == 0)
        {
            Debug.LogWarning("No se pudo encontrar un camino accesible.");
        }
    }

    private void MoverHaciaWaypoint(Waypoint waypointActual)
    {
        Vector3 targetDirection = (waypointActual.transform.position - transform.position).normalized;
        Vector3 adjustedDirection = obstacleAvoidance.GetAdjustedDirection(transform, targetDirection, rayDistance);

        // Si no hay direcci�n ajustada, recalcula el camino
        if (adjustedDirection == Vector3.zero)
        {
            Debug.LogWarning("Obst�culo detectado. Recalculando camino...");
            ActualizarCamino();
            return;
        }

        transform.position += adjustedDirection * velocidad * Time.deltaTime;

        // Si el enemigo est� cerca del waypoint, avanza al siguiente
        if (Vector3.Distance(transform.position, waypointActual.transform.position) < 0.5f)
        {
            waypointIndex++;
        }
    }

    private void SeguirCaminoDijkstra()
    {
        if (camino == null || camino.Count == 0)
        {
            ActualizarCamino();
            return;
        }

        // Verifica si el �ndice es v�lido
        if (waypointIndex < camino.Count)
        {
            Waypoint waypointActual = camino[waypointIndex];

            Vector3 targetDirection = (waypointActual.transform.position - transform.position).normalized;
            Vector3 adjustedDirection = obstacleAvoidance.GetAdjustedDirection(transform, targetDirection, rayDistance);

            // Si no hay direcci�n ajustada, intenta recalcular el camino
            if (adjustedDirection == Vector3.zero)
            {
                Debug.LogWarning("Obst�culo detectado. Recalculando camino...");
                ActualizarCamino();
                return;
            }

            transform.position += adjustedDirection * velocidad * Time.deltaTime;

            // Si el enemigo est� cerca del waypoint, avanza al siguiente
            if (Vector3.Distance(transform.position, waypointActual.transform.position) < Mathf.Max(0.5f, velocidad * Time.deltaTime))
            {
                waypointIndex++;

                // Si alcanz� el �ltimo waypoint, recalcula el camino
                if (waypointIndex >= camino.Count)
                {
                    Debug.Log("Camino completado. Recalculando...");
                    ActualizarCamino();
                }
            }
        }
        else
        {
            // Si el �ndice es inv�lido, recalcula el camino
            Debug.LogWarning("�ndice de waypoint fuera de rango. Recalculando...");
            ActualizarCamino();
        }
    }

    private void NavegarEntreWaypoints()
    {
        Waypoint waypointMasCercano = ObtenerWaypointMasCercano(transform.position);

        if (waypointMasCercano == null)
        {
            Debug.LogWarning("No se encontr� un waypoint cercano.");
            return;
        }

        Vector3 targetDirection = (waypointMasCercano.transform.position - transform.position).normalized;
        Vector3 adjustedDirection = obstacleAvoidance.GetAdjustedDirection(transform, targetDirection, rayDistance);

        if (adjustedDirection != Vector3.zero)
        {
            transform.position += adjustedDirection * velocidad * Time.deltaTime;
        }
        else
        {
            Debug.LogWarning("Obst�culo detectado en modo Waypoints.");
        }
    }


    private bool EsWaypointAccesible(Vector3 desde, Vector3 hasta)
    {
        Vector3 direccion = hasta - desde;
        float distancia = Vector3.Distance(desde, hasta);
        RaycastHit2D hit = Physics2D.Raycast(desde, direccion, distancia, wallLayer);

        return hit.collider == null; // True si no hay colisi�n
    }
    private Waypoint BuscarWaypointAlternativo(Waypoint waypointInaccesible)
    {
        foreach (var vecino in graphController.GetGraph().GetVecinos(waypointInaccesible))
        {
            if (EsWaypointAccesible(transform.position, vecino.transform.position))
            {
                return vecino;
            }
        }

        return null; // No se encontr� un waypoint alternativo
    }
    private Waypoint ObtenerWaypointMasCercano(Vector3 posicion)
    {
        Waypoint closestWaypoint = null;
        float minDistancia = Mathf.Infinity;

        foreach (Waypoint waypoint in graphController.GetGraph().GetNodos())
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

    private void OnDrawGizmosSelected()
    {
        if (camino != null && camino.Count > 1)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < camino.Count - 1; i++)
            {
                Gizmos.DrawLine(camino[i].transform.position, camino[i + 1].transform.position);
            }
        }
    }
}