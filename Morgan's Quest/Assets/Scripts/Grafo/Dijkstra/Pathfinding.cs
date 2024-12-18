using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private GraphController graphController;

    public Pathfinding(GraphController graphController)
    {
        this.graphController = graphController;
    }

    public List<Waypoint> Dijkstra(Waypoint inicio, Waypoint destino, LayerMask wallLayer)
    {
        var grafo = graphController.GetGraph();
        var distancias = new Dictionary<Waypoint, float>();
        var previos = new Dictionary<Waypoint, Waypoint>();
        var visitados = new HashSet<Waypoint>();
        var cola = new MinHeap<Waypoint>();

        // Precalcular las aristas accesibles
        var aristasAccesibles = new Dictionary<Waypoint, List<Waypoint>>();
        foreach (var nodo in grafo.GetNodos())
        {
            aristasAccesibles[nodo] = new List<Waypoint>();
            foreach (var arista in grafo.GetAristas())
            {
                if (arista.source == nodo)
                {
                    Waypoint vecino = arista.destination;

                    // Asegurarnos de que el camino entre los waypoints esté libre de obstáculos
                    if (EsWaypointAccesible(nodo, vecino, wallLayer))
                    {
                        aristasAccesibles[nodo].Add(vecino);
                    }
                }
            }
        }

        // Inicializar las distancias y la cola de prioridad
        foreach (var nodo in grafo.GetNodos())
        {
            distancias[nodo] = float.MaxValue;
            previos[nodo] = null;
        }

        distancias[inicio] = 0;
        cola.Add(0, inicio);

        // Algoritmo de Dijkstra
        while (cola.Count > 0)
        {
            var actual = cola.ExtractMin().item;

            if (actual == destino) break;

            if (visitados.Contains(actual)) continue;
            visitados.Add(actual);

            foreach (var vecino in aristasAccesibles[actual])
            {
                float peso = Vector3.Distance(actual.transform.position, vecino.transform.position); // O el peso que corresponda
                float nuevaDistancia = distancias[actual] + peso;

                if (nuevaDistancia < distancias[vecino])
                {
                    distancias[vecino] = nuevaDistancia;
                    previos[vecino] = actual;
                    cola.Add((int)nuevaDistancia, vecino);
                }
            }
        }

        // Reconstruir el camino
        var camino = new List<Waypoint>();
        for (var nodo = destino; nodo != null; nodo = previos[nodo])
        {
            camino.Insert(0, nodo);
        }

        return camino;
    }

    private bool EsWaypointAccesible(Waypoint desde, Waypoint hasta, LayerMask wallLayer)
    {
        Vector3 direccion = hasta.transform.position - desde.transform.position;
        float distancia = Vector3.Distance(desde.transform.position, hasta.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(desde.transform.position, direccion, distancia, wallLayer);

        return hit.collider == null; // Devuelve true si no hay colisión
    }
}
