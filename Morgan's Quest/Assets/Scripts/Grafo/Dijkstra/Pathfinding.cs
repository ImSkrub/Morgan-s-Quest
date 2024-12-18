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

    public List<Waypoint> Dijkstra(Waypoint inicio, Waypoint destino)
    {
        var grafo = graphController.GetGraph();
        var distancias = new Dictionary<Waypoint, float>();
        var previos = new Dictionary<Waypoint, Waypoint>();
        var visitados = new HashSet<Waypoint>();
        var cola = new MinHeap<Waypoint>();

        foreach (var nodo in grafo.GetNodos())
        {
            distancias[nodo] = float.MaxValue;
            previos[nodo] = null;
        }

        distancias[inicio] = 0;
        cola.Add(0, inicio);

        while (cola.Count > 0)
        {
            var actual = cola.ExtractMin().item;

            if (actual == destino) break;

            if (visitados.Contains(actual)) continue;
            visitados.Add(actual);

            foreach (var arista in grafo.GetAristas())
            {
                if (arista.source == actual)
                {
                    Waypoint vecino = arista.destination;
                    float peso = arista.weight;
                    float nuevaDistancia = distancias[actual] + peso;

                    if (nuevaDistancia < distancias[vecino])
                    {
                        distancias[vecino] = nuevaDistancia;
                        previos[vecino] = actual;
                        cola.Add((int)nuevaDistancia, vecino);
                    }
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
}
