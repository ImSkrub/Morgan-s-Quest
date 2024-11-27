using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

        public class Dijkstra : MonoBehaviour
        {
            public (Dictionary<Vertice, int> distances, Dictionary<Vertice, Vertice> previous) ShortestPaths(Graph graph, int sourceValue)
            {
                // Verificar si el v�rtice fuente existe
                if (!graph.Vertices.ContainsKey(sourceValue))
                {
                    Debug.LogWarning($"Source vertex {sourceValue} not found in the graph.");
                    return (null, null); // V�rtice fuente no encontrado
                }

                var distances = new Dictionary<Vertice, int>();
                var previous = new Dictionary<Vertice, Vertice>();
                var priorityQueue = new MinHeap<Vertice>();
                var visited = new HashSet<Vertice>(); // Conjunto para rastrear v�rtices visitados

                // Inicializar distancias y la cola de prioridad
                foreach (var vertex in graph.Vertices.Values)
                {
                    distances[vertex] = int.MaxValue; // Inicializar todas las distancias a infinito
                    previous[vertex] = null;
                    priorityQueue.Add(int.MaxValue, vertex); // Agregar al heap con distancia m�xima inicialmente
                }

                // Establecer la distancia al v�rtice fuente en 0
                var sourceVertex = graph.Vertices[sourceValue];
                distances[sourceVertex] = 0;
                priorityQueue.Add(0, sourceVertex);

                while (priorityQueue.Count > 0)
                {
                    // Extraer el v�rtice con la distancia m�s peque�a
                    var (currentDistance, currentVertex) = priorityQueue.ExtractMin();

                    // Si el v�rtice ya ha sido visitado, continuar
                    if (visited.Contains(currentVertex))
                    {
                        continue;
                    }

                    visited.Add(currentVertex); // Marcar el v�rtice como visitado

                    // Depuraci�n: registrar el v�rtice actual y su distancia
                    Debug.Log($"Current Vertex: {currentVertex.Value}, Distance: {currentDistance}");

                    // Iterar sobre las aristas del v�rtice actual
                    foreach (var edge in currentVertex.aristas)
                    {
                        int newDistance = currentDistance + edge.Weight;

                        // Depuraci�n: registrar el nuevo c�lculo de distancia
                        Debug.Log($"Checking edge from {currentVertex.Value} to {edge.Destination.Value}, New Distance: {newDistance}");

                        // Si la nueva distancia es menor, actualizar la distancia y el predecesor
                        if (newDistance < distances[edge.Destination])
                        {
                            distances[edge.Destination] = newDistance;
                            previous[edge.Destination] = currentVertex;

                            // Agregar la distancia actualizada a la cola de prioridad
                            priorityQueue.Add(newDistance, edge.Destination);
                        }
                    }
                }

                // Depuraci�n: mostrar las distancias finales y los predecesores
                foreach (var kvp in distances)
                {
                    Debug.Log($"Distance to {kvp.Key.Value}: {kvp.Value}");
                }

                foreach (var kvp in previous)
                {
                    Debug.Log($"Previous of {kvp.Key.Value}: {kvp.Value?.Value}");
                }

                return (distances, previous); // Retornar distancias y predecesores
            }
        }
