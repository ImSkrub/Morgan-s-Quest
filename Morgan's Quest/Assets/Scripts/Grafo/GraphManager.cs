using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    private Graph graph;
    private Dijkstra dijkstra;
    private WaypointManager waypointManager;

    private void Start()
    {
        // Inicializa el WaypointManager
        waypointManager = FindObjectOfType<WaypointManager>();
        if (waypointManager == null)
        {
            Debug.LogError("WaypointManager not found!");
            return;
        }
        InitializeGraph(); // Método que inicializa los vértices y aristas

        // Inicializa el grafo
        graph = gameObject.AddComponent<Graph>(); // Agrega el componente Graph al mismo GameObject
    }

    private void InitializeGraph()
    {
        if (waypointManager.Waypoints == null || waypointManager.Waypoints.Count == 0)
        {
            Debug.LogError("No waypoints available in WaypointManager.");
            return;
        }

        Debug.Log($"Number of waypoints: {waypointManager.Waypoints.Count}");

        // Agrega los waypoints como vértices
        foreach (var waypoint in waypointManager.Waypoints)
        {
            graph.AddVertice(waypoint.gameObject.GetInstanceID());
        }
        Debug.Log($"Number of waypoints: {waypointManager.Waypoints.Count}");

        // Agrega los waypoints como vértices
        foreach (var waypoint in waypointManager.Waypoints)
        {
            graph.AddVertice(waypoint.gameObject.GetInstanceID());
        }

        // Agrega el jugador como vértice
        int playerWaypointId = waypointManager.GetPlayerWaypointId();
        if (playerWaypointId != -1)
        {
            graph.AddVertice(playerWaypointId);
        }

        // Agrega las aristas entre waypoints
        for (int i = 0; i < waypointManager.Waypoints.Count; i++)
        {
            for (int j = i + 1; j < waypointManager.Waypoints.Count; j++)
            {
                var waypoint1 = waypointManager.Waypoints[i];
                var waypoint2 = waypointManager.Waypoints[j];
                float weight = Vector2.Distance(waypoint1.transform.position, waypoint2.transform.position);

                graph.AddArista(
                    waypoint1.gameObject.GetInstanceID(),
                    waypoint2.gameObject.GetInstanceID(),
                    Mathf.RoundToInt(weight)
                );
            }
        }

        // Agrega las aristas entre el jugador y los waypoints
        if (playerWaypointId != -1)
        {
            var playerPosition = waypointManager.Player.transform.position;
            foreach (var waypoint in waypointManager.Waypoints)
            {
                float weight = Vector2.Distance(playerPosition, waypoint.transform.position);

                graph.AddArista(
                    playerWaypointId,
                    waypoint.gameObject.GetInstanceID(),
                    Mathf.RoundToInt(weight)
                );
            }
        }
    }

    public int GetPlayerWaypointId()
    {
        if (waypointManager.Player != null)
        {
            Transform playerWaypoint = waypointManager.Player.transform.Find("Waypoint");
            if (playerWaypoint != null)
            {
                Debug.Log($"Player waypoint ID: {playerWaypoint.gameObject.GetInstanceID()}");
                return playerWaypoint.gameObject.GetInstanceID();
            }
            Debug.LogWarning("Player's waypoint not found!");
            return -1;
        }
        
        var playerPosition = waypointManager.Player.transform.position;
        int closestWaypointId = -1;
        float closestDistance = float.MaxValue;

        // Find the closest waypoint to the player
        foreach (var waypoint in waypointManager.Waypoints)
        {
            float distance = Vector2.Distance(playerPosition, waypoint.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWaypointId = waypoint.gameObject.GetInstanceID();
            }
        }

        return closestWaypointId;
    }

    public (Dictionary<Vertice, int> distances, Dictionary<Vertice, Vertice> previous) GetShortestPaths(int sourceValue)
    {
        dijkstra = gameObject.AddComponent<Dijkstra>();
        return dijkstra.ShortestPaths(graph, sourceValue);
    }

    public Vertice GetVertice(int instanceId)
    {
        return graph.GetVertice(instanceId);
    }
}
