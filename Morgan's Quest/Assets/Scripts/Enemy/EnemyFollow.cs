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

    [SerializeField] private LayerMask wallLayer;
    public List<Waypoint> camino;
    private int waypointIndex = 0;
    private float tiempoUltimoRecalculo;

    private IObstacleAvoidance obstacleAvoidance;

    private void Awake()
    {
        graphController = FindAnyObjectByType<GraphController>();

        if (graphController == null)
        {
            Debug.LogError("GraphController no está asignado.");
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

    void Update()
    {
        base.Update();

        if (camino == null || camino.Count == 0) return;

        RecalculatePathIfNeeded();
        MoverHaciaWaypoint();
       
    }

    private void RecalculatePathIfNeeded()
    {
        bool jugadorSeMovio = Vector3.Distance(jugador.position, ultimaPosicionJugador) > 1f;
        bool tiempoExcedido = Time.time - tiempoUltimoRecalculo > tiempoRecalculo;
        bool waypointInaccesible = waypointIndex < camino.Count && !EsWaypointAccesible(transform.position, camino[waypointIndex].transform.position);

        if (jugadorSeMovio || tiempoExcedido || waypointInaccesible || waypointIndex >= camino.Count)
        {
            ActualizarCamino();
            ultimaPosicionJugador = jugador.position;
            tiempoUltimoRecalculo = Time.time;
        }
    }

    public void ActualizarCamino()
    {
        Waypoint waypointInicio = ObtenerWaypointMasCercano(transform.position);
        Waypoint waypointDestino = ObtenerWaypointMasCercano(jugador.position);

        // Usamos Dijkstra para encontrar el camino más corto entre los waypoints
        Pathfinding pathfinding = new Pathfinding(graphController);
        camino = pathfinding.Dijkstra(waypointInicio, waypointDestino, wallLayer);

        // Reiniciamos el índice del waypoint
        waypointIndex = 0;

        if (camino.Count == 0)
        {
            Debug.LogWarning("No se pudo encontrar un camino accesible.");
        }
    }

    private void MoverHaciaWaypoint()
    {
        if (waypointIndex < camino.Count)
        {
            Waypoint waypointActual = camino[waypointIndex];

            Vector3 targetDirection = (waypointActual.transform.position - transform.position).normalized;
            Vector3 adjustedDirection = obstacleAvoidance.GetAdjustedDirection(transform, targetDirection, rayDistance);

            if (adjustedDirection == Vector3.zero)
            {
                // Si no hay dirección accesible, recalculamos el camino
                Debug.Log("Rayo detecta obstáculo. Recalculando camino...");
                ActualizarCamino();
                return;
            }

            // Mover hacia la dirección ajustada
            transform.position += adjustedDirection * velocidad * Time.deltaTime;

            // Avanzar al siguiente waypoint si estamos cerca
            if (Vector3.Distance(transform.position, waypointActual.transform.position) < 0.5f)
            {
                waypointIndex++;
            }
        }
        else
        {
            // Si no hay más waypoints, recalcular camino
            ActualizarCamino();
        }
    }


    private bool EsWaypointAccesible(Vector3 desde, Vector3 hasta)
    {
        Vector3 direccion = hasta - desde;
        float distancia = Vector3.Distance(desde, hasta);
        RaycastHit2D hit = Physics2D.Raycast(desde, direccion, distancia, wallLayer);

        return hit.collider == null; // True si no hay colisión
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