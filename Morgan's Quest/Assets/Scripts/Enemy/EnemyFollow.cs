using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    private GraphManager graphManager;
    private List<Vertice> path = new List<Vertice>();
    private int currentWaypointIndex = 0;
    public float avoidanceRayLength = 1.5f; // Length of the ray used to detect obstacles

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
            Vector2 directionToMove = (targetWaypoint.Position - (Vector2)transform.position).normalized;

            // Check for obstacles before moving
            if (IsObstacleInPath(directionToMove))
            {
                AvoidObstacle(directionToMove);
            }
            else
            {
                MoveTowardsWaypoint(targetWaypoint);
            }

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
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, vel * Time.deltaTime);
    }

    // Check if there is an obstacle in the way using Raycasting
    private bool IsObstacleInPath(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, avoidanceRayLength, wallLayer);
        return hit.collider != null;
    }

    // Adjust direction to avoid obstacle (bounce off or steer)
    private void AvoidObstacle(Vector2 direction)
    {
        // If obstacle detected, try to move in a different direction
        // Cast multiple rays to check for left or right movement to avoid the obstacle
        Vector2 avoidanceDirection = direction;

        // Cast to the left
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, -Vector2.right, avoidanceRayLength, wallLayer);
        // Cast to the right
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, avoidanceRayLength, wallLayer);

        // If both directions are clear, move in the original direction
        if (leftHit.collider == null && rightHit.collider == null)
        {
            avoidanceDirection = direction;
        }
        else
        {
            // If left side is clear, move left
            if (leftHit.collider == null)
            {
                avoidanceDirection = -Vector2.right;
            }
            // If right side is clear, move right
            else if (rightHit.collider == null)
            {
                avoidanceDirection = Vector2.right;
            }
        }

        // Apply the new avoidance direction
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + avoidanceDirection, vel* Time.deltaTime);
    }
}
