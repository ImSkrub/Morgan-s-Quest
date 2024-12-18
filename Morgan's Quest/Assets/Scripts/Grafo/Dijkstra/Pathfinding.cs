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
        var weights = new Dictionary<int, int>();
        var previous = new Dictionary<int, Waypoint>();
        var visited = new HashSet<int>();
        var priorityQueue = new MinHeap<int>();

        foreach (var nodo in graphController.waypointInScene)
        {
            weights[nodo.Key] = int.MaxValue;
            previous[nodo.Key] = null;
        }

        weights[start.iD] = 0;
        priorityQueue.Add(0, start.iD);

        while (priorityQueue.Count > 0)
        {
            var (currentWeight, currentNodeId) = priorityQueue.ExtractMin();
            var currentNode = graphController.waypointInScene[currentNodeId];

            if (visited.Contains(currentNodeId)) continue;

            visited.Add(currentNodeId);

            if (currentNode == target) break;

            foreach (var arista in currentNode.aristasConectadas)
            {
                var neighbourNode = arista.destination;
                int neighbourNodeId = neighbourNode.iD;

                if (visited.Contains(neighbourNodeId)) continue;

                int weight = arista.weight;
                int newDist = weights[currentNodeId] + weight;

                if (newDist < weights[neighbourNodeId])
                {
                    weights[neighbourNodeId] = newDist;
                    previous[neighbourNodeId] = currentNode;
                    priorityQueue.Add(newDist, neighbourNodeId);
                }
            }
        }

        var path = new List<Waypoint>();
        Waypoint current = target;

        while (current != null)
        {
            path.Add(current);
            current = previous.ContainsKey(current.iD) ? previous[current.iD] : null;
        }

        path.Reverse();

        return path;
    }
}
