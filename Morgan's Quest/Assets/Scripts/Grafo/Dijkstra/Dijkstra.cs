using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dijkstra : MonoBehaviour
{
    public Dictionary<Vertice, int> ShortestPaths(Graph graph, int sourceValue)
    {
        if (!graph.Vertices.ContainsKey(sourceValue))
        {
            return null; // Source vertex not found
        }

        var distances = new Dictionary<Vertice, int>();
        var previous = new Dictionary<Vertice, Vertice>();
        var priorityQueue = new List<(int distance, Vertice vertex)>();

        foreach (var vertex in graph.Vertices.Values)
        {
            distances[vertex] = int.MaxValue;
            previous[vertex] = null;
            priorityQueue.Add((int.MaxValue, vertex));
        }

        distances[graph.Vertices[sourceValue]] = 0;
        priorityQueue.Add((0, graph.Vertices[sourceValue]));

        while (priorityQueue.Count > 0)
        {
            // Sort the priority queue to find the vertex with the smallest distance
            priorityQueue.Sort((a, b) => a.distance.CompareTo(b.distance));
            var (currentDistance, currentVertex) = priorityQueue[0];
            priorityQueue.RemoveAt(0); // Remove the vertex with the smallest distance

            // Debugging: Log the current vertex and its distance
            Debug.Log($"Current Vertex: {currentVertex.Value}, Distance: {currentDistance}");


            foreach (var edge in currentVertex.aristas)
            {
                int newDistance = currentDistance + edge.Weight;

                // Debugging: Log the new distance calculation
                Debug.Log($"Checking edge from {currentVertex.Value} to {edge.Destination.Value}, New Distance: {newDistance}");

                if (newDistance < distances[edge.Destination])
                {
                    // Update the distance and previous vertex
                    distances[edge.Destination] = newDistance;
                    previous[edge.Destination] = currentVertex;

                    // Update the priority queue
                    priorityQueue.RemoveAll(x => x.vertex == edge.Destination);
                    priorityQueue.Add((newDistance, edge.Destination));
                }
            }
        }

        return distances;
    }
}
