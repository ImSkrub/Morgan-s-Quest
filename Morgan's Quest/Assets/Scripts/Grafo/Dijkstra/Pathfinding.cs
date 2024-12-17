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

    public List<Waypoint> Dijkstra(Waypoint start, Waypoint target)
    {
        var weights = new Dictionary<int, int>(); // Almacena las distancias mínimas
        var previous = new Dictionary<int, Waypoint>();  // Almacena el nodo anterior
        var visited = new HashSet<int>();  // Nodos visitados
        var priorityQueue = new PriorityQueueTDA<int>(); // Cola de prioridad

        // Inicializa todos los nodos con una distancia infinita
        foreach (var nodo in graphController.waypointInScene)
        {
            weights[nodo.Key] = int.MaxValue;
            previous[nodo.Key] = null;
        }

        // Establece el peso inicial para el nodo de inicio
        weights[start.iD] = 0;
        priorityQueue.Enqueue(start.iD, weights[start.iD]);

        while (priorityQueue.Count() > 0)
        {
            var currentNodeId = priorityQueue.Dequeue();
            var currentNode = graphController.waypointInScene[currentNodeId];

            // Si el nodo ya ha sido visitado, continúa con el siguiente
            if (visited.Contains(currentNodeId)) continue;

            visited.Add(currentNodeId);

            // Si se ha alcanzado el nodo objetivo, termina
            if (currentNode == target) break;

            // Procesa los vecinos del nodo actual
            foreach (var arista in currentNode.aristasConectadas)
            {
                var neighbourNode = arista.destination;
                int neighbourNodeId = neighbourNode.iD;

                // Si el nodo vecino ya fue visitado, lo saltamos
                if (visited.Contains(neighbourNodeId)) continue;

                int weight = arista.weight;  // Peso de la arista (distancia entre nodos)
                int newDist = weights[currentNodeId] + weight;  // Suma la distancia actual al peso del vecino

                // Si encontramos una ruta más corta, actualizamos la distancia y el nodo anterior
                if (newDist < weights[neighbourNodeId])
                {
                    weights[neighbourNodeId] = newDist;
                    previous[neighbourNodeId] = currentNode;
                    priorityQueue.Enqueue(neighbourNodeId, newDist);
                }
            }
        }

        // Generar el camino desde el nodo objetivo hacia el nodo de inicio
        var path = new List<Waypoint>();

        // Asegurarse de que el nodo de destino tiene un valor válido
        Waypoint current = target;
        while (current != null)
        {
            path.Add(current);
            current = previous.ContainsKey(current.iD) ? previous[current.iD] : null;
        }

        path.Reverse();  // Revertir el camino para que vaya de inicio a destino

        return path;
    }
}
