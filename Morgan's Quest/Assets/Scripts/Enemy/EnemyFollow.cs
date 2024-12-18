using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    public GraphController graphController;
    public Transform jugador;
    public float velocidad = 3f;
    public float rayDistance = 1f;
    public float tiempoRecalculo = 1f;

    [SerializeField] private LayerMask wallLayer;
    private List<Waypoint> camino;
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
        tiempoUltimoRecalculo = Time.time;
    }

    void Update()
    {
        base.Update();

        if (camino.Count == 0) return;

        MoverHaciaWaypoint();

        // Si no alcanzamos el waypoint en el tiempo límite, recalculamos el camino
        if (Time.time - tiempoUltimoRecalculo > tiempoRecalculo)
        {
            ActualizarCamino();
            tiempoUltimoRecalculo = Time.time;
        }

        if (waypointIndex < camino.Count && Vector3.Distance(transform.position, camino[waypointIndex].transform.position) < 0.5f)
        {
            waypointIndex++;
            if (waypointIndex >= camino.Count)
            {
                ActualizarCamino();
                tiempoUltimoRecalculo = Time.time;
            }
        }
    }

    private void ActualizarCamino()
    {
        Waypoint waypointInicio = ObtenerWaypointMasCercano(transform.position);
        Waypoint waypointDestino = ObtenerWaypointMasCercano(jugador.position);

        Pathfinding pathfinding = new Pathfinding(graphController);
        camino = pathfinding.Dijkstra(waypointInicio, waypointDestino);

        waypointIndex = 0;
    }

    private void MoverHaciaWaypoint()
    {
        if (waypointIndex < camino.Count)
        {
            Waypoint waypointActual = camino[waypointIndex];
            Waypoint waypointSiguiente = waypointIndex + 1 < camino.Count ? camino[waypointIndex + 1] : null;

            if (waypointSiguiente != null)
            {
                Vector3 targetDirection = (waypointSiguiente.transform.position - transform.position).normalized;

                // Ajustamos la dirección usando la evasión de obstáculos
                Vector3 adjustedDirection = obstacleAvoidance.GetAdjustedDirection(transform, targetDirection, rayDistance);
                transform.position += adjustedDirection * velocidad * Time.deltaTime;
            }
            else
            {
                // Si no hay waypoint siguiente, simplemente nos movemos hacia el waypoint actual
                Vector3 targetDirection = (waypointActual.transform.position - transform.position).normalized;
                Vector3 adjustedDirection = obstacleAvoidance.GetAdjustedDirection(transform, targetDirection, rayDistance);
                transform.position += adjustedDirection * velocidad * Time.deltaTime;
            }
        }
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
}