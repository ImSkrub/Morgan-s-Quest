using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Dictionary<int, Vertice> Vertices { get; private set; }

    private void Awake()
    {
        Vertices = new Dictionary<int, Vertice>();
        Debug.Log("Graph initialized. Vertices dictionary created.");
    }

    public void AddVertice(int value)
    {
        if (!Vertices.ContainsKey(value))
        {
            Vertice newVertex = new GameObject($"Vertice{value}").AddComponent<Vertice>();
            newVertex.Initialize(value);
            Vertices[value] = newVertex;
            Debug.Log($"Vertex added: {value}");
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
            if (!Vertices.ContainsKey(sourceValue))
            {
                Debug.LogWarning($"Source vertex {sourceValue} does not exist. Cannot add edge.");
            }
            if (!Vertices.ContainsKey(destinationValue))
            {
                Debug.LogWarning($"Destination vertex {destinationValue} does not exist. Cannot add edge.");
            }
        }
    }

    public Vertice GetVertice(int instanceId)
    {
        if (Vertices.TryGetValue(instanceId, out var vertice))
        {
            Debug.Log($"Vertex {instanceId} retrieved.");
            return vertice;
        }
        else
        {
            Debug.LogWarning($"Vertex {instanceId} not found.");
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (Vertices == null) return;

        Gizmos.color = Color.red; // Color para las aristas

        foreach (var vertice in Vertices.Values)
        {
            foreach (var arista in vertice.aristas)
            {
                Gizmos.DrawLine(vertice.transform.position, arista.Destination.transform.position);
            }
        }
    }
}
