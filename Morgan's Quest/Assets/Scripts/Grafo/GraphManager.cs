using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    private void Start()
    {
        Graph graph = gameObject.AddComponent<Graph>();
        //Agrego vertices
        graph.AddVertice(1);
        graph.AddVertice(2);
        graph.AddVertice(3);
        graph.AddVertice(4);

        //Agrego aristas primero origen, segundo destino, tercero peso arista.

        graph.AddArista(1, 2, 1);
        graph.AddArista(1, 3, 4);
        graph.AddArista(2, 3, 2);
        graph.AddArista(2, 4, 5);
        graph.AddArista(3, 4, 1);

        Dijkstra dijkstra = gameObject.AddComponent<Dijkstra>();
        var shortestPaths = dijkstra.ShortestPaths(graph, 1);

        foreach(var kvp in shortestPaths)
        {
            Debug.Log($"Distance from 1 to {kvp.Key.Value}: {kvp.Value}");
        }
    }
}
