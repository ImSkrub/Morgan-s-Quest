using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    private GraphManager graphManager;
    private List<Vertice> path = new List<Vertice>();
    private int currentWaypointIndex = 0;
    public float speed = 2;
    protected override void Start()
    {
        base.Start();
        graphManager = FindObjectOfType<GraphManager>();

        if (graphManager != null)
        {
            CalculatePathToPlayer();
        }
    }

    private void Update()
    {
        base.Update();

        if (path.Count > 0 && currentWaypointIndex < path.Count)
        {
            Vertice targetWaypoint = path[currentWaypointIndex];
            MoveTowardsWaypoint(targetWaypoint);

            if (Vector2.Distance(transform.position, targetWaypoint.Position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            CalculatePathToPlayer();
        }
    }

    private void CalculatePathToPlayer()
    {
        Graph graph = graphManager.GetGraph();
        if (graph == null || player == null) return;

        int enemyVerticeId = GetClosestVerticeId();
        int playerVerticeId = player.gameObject.GetInstanceID();

        Dijkstra dijkstra = gameObject.AddComponent<Dijkstra>();
        var (distances, previous) = dijkstra.ShortestPaths(graph, enemyVerticeId);

        path.Clear();
        Vertice current = graph.Vertices[playerVerticeId];
        while (current != null)
        {
            path.Insert(0, current);
            previous.TryGetValue(current, out current);
        }

        currentWaypointIndex = 0;
    }

    private int GetClosestVerticeId()
    {
        Graph graph = graphManager.GetGraph();
        Vertice closest = null;
        float minDistance = float.MaxValue;

        foreach (var vertice in graph.Vertices.Values)
        {
            float distance = Vector2.Distance(transform.position, vertice.Position);
            if (distance < minDistance)
            {
                closest = vertice;
                minDistance = distance;
            }
        }

        return closest?.Value ?? -1;
    }

    private void MoveTowardsWaypoint(Vertice targetWaypoint)
    {
        Vector2 targetPosition = targetWaypoint.Position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
