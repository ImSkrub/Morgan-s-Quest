using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Dictionary<int, Vertice> Vertices { get; private set; }

    private void Awake()
    {
        Vertices = new Dictionary<int, Vertice>();
        Debug.Log("Graph initialized.");
    }

    public void AddVertice(GameObject waypoint)
    {
        int value = waypoint.GetInstanceID();

        if (!Vertices.ContainsKey(value))
        {
            Vertice newVertex = waypoint.AddComponent<Vertice>();
            newVertex.Initialize(value);
            Vertices[value] = newVertex;

            Debug.Log($"Vertex added and linked to Waypoint: {waypoint.name}");
        }
        else
        {
            Debug.LogWarning($"Vertex {value} already exists. Skipping addition.");
        }
    }

    public void AddArista(int sourceValue, int destinationValue, int weight)
    {
        if (Vertices.ContainsKey(sourceValue) && Vertices.ContainsKey(destinationValue))
        {
            Vertices[sourceValue].AddArista(Vertices[destinationValue], weight);
            Debug.Log($"Edge added from {sourceValue} to {destinationValue} with weight {weight}");
        }
        else
        {
            Debug.LogWarning($"Cannot add edge: Source or destination vertex not found.");
        }
    }
}
