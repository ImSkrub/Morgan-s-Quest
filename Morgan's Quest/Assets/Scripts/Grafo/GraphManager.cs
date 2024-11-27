using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    private Graph graph;
    private Dijkstra dijkstra;

    private void Start()
    {
        graph = gameObject.AddComponent<Graph>();

        // Encuentra todos los objetos Waypoint en la escena
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        // Agrega los waypoints como vértices al grafo
        foreach (var waypoint in waypoints)
        {
            graph.AddVertice(waypoint.gameObject.GetInstanceID()); // Usamos el ID de la instancia como valor
        }

        // Agrega el jugador como un vértice
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            graph.AddVertice(player.GetInstanceID()); // Usamos el ID de la instancia como valor
            AddPlayerWaypoint(player); // Asegúrate de agregar el waypoint del jugador
        }

        // Agrega aristas entre los waypoints con peso 1
        for (int i = 0; i < waypoints.Length; i++)
        {
            for (int j = i + 1; j < waypoints.Length; j++)
            {
                // Asignar peso 1 a todas las aristas
                graph.AddArista(waypoints[i].gameObject.GetInstanceID(), waypoints[j].gameObject.GetInstanceID(), 1);
            }
        }

        // Agrega aristas entre el jugador y los waypoints con peso 1
        if (player != null)
        {
            foreach (var waypoint in waypoints)
            {
                graph.AddArista(player.GetInstanceID(), waypoint.gameObject.GetInstanceID(), 1);
            }
        }

        if (dijkstra == null)
        {
            dijkstra = gameObject.AddComponent<Dijkstra>();
        }
        var (shortestPaths, predecessors) = dijkstra.ShortestPaths(graph, player.GetInstanceID());

        foreach (var kvp in shortestPaths)
        {
            Debug.Log($"Distance from Player to {kvp.Key.Value}: {kvp.Value}");
        }
    }

    public void AddPlayerWaypoint(GameObject player)
    {
        if (player != null)
        {
            // Suponiendo que el waypoint es un hijo del jugador
            Transform playerWaypoint = player.transform.Find("Waypoint"); // Cambia "Waypoint" por el nombre real del objeto
            if (playerWaypoint != null)
            {
                graph.AddVertice(playerWaypoint.GetInstanceID()); // Agregar el waypoint del jugador al grafo
            }
            else
            {
                Debug.LogWarning("Waypoint not found as a child of the player!");
            }
        }
    }

    public (Dictionary<Vertice, int> distances, Dictionary<Vertice, Vertice> previous) GetShortestPaths(int sourceValue)
    {
        dijkstra = gameObject.AddComponent<Dijkstra>();
        return dijkstra.ShortestPaths(graph, sourceValue);
    }

    public Vertice GetVertice(int instanceId)
    {
        return graph.GetVertice(instanceId); // Método para obtener un vértice por su ID
    }

    public Vertice GetSpecificWaypoint(GameObject player)
    {
        if (player != null)
        {
            Transform playerWaypoint = player.transform.Find("Waypoint"); // Cambia "Waypoint" por el nombre real del objeto
            if (playerWaypoint != null)
            {
                return GetVertice(playerWaypoint.GetInstanceID());
            }
            else
            {
                Debug.LogWarning("Specific waypoint not found for the player!");
            }
        }
        return null;
    }
}
