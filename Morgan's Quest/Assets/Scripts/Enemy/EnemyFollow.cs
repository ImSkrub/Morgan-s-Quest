using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFollow : Enemy
{
    private List<Vertice> path = new List<Vertice>();
    private int currentWaypointIndex = 0;
    private GraphManager graphManager; // Almacenar referencia a GraphManager
    private float pathUpdateThreshold = 0.5f; // Distancia mínima para actualizar el camino

    private void Start()
    {
        base.Start();
        graphManager = FindObjectOfType<GraphManager>(); // Get reference to GraphManager once
        UpdatePath(); // Initialize the path to the player
    }

    private void Update()
    {
        base.Update();

        UpdatePath();

        if (player != null)
        {
            if (path.Count > 0 && currentWaypointIndex < path.Count)
            {
                MoveTowardsWaypoint(path[currentWaypointIndex]);
                Debug.Log($"Moving towards waypoint: {path[currentWaypointIndex].Value}");

                if (Vector2.Distance(transform.position, path[currentWaypointIndex].transform.position) < 0.1f)
                {
                    currentWaypointIndex++;
                    Debug.Log("Reached waypoint!");
                }
            }
            else
            {
                Debug.LogWarning("Path is empty or current waypoint index is out of range.");
            }
        }
        else
        {
            Debug.LogWarning("Player reference is null!");
        }
    }


    private void MoveTowardsWaypoint(Vertice targetWaypoint)
    {
        if (targetWaypoint == null) return;

        Vector2 targetPosition = targetWaypoint.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, vel * Time.deltaTime);
        if (currentWaypointIndex < path.Count)
        {
            MoveTowardsWaypoint(path[currentWaypointIndex]);
        }
        else
        {
            Debug.LogWarning("Current waypoint index is out of range!");
        }

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            Debug.Log("Reached target waypoint!");
            // Aquí puedes agregar lógica adicional si es necesario
        }
    }
    public void UpdatePath()
    {
        int playerWaypointId = graphManager.GetPlayerWaypointId();
        if (playerWaypointId != -1)
        {
            var (shortestPaths, predecessors) = graphManager.GetShortestPaths(playerWaypointId);

            Vertice enemyStartWaypoint = graphManager.GetVertice(gameObject.GetInstanceID());
            Debug.Log($"Enemy instance ID: {gameObject.GetInstanceID()}, looking for vertex.");
            if (enemyStartWaypoint != null)
            {
                path.Clear();

                Vertice currentVertex = enemyStartWaypoint;
                while (currentVertex != null && predecessors.ContainsKey(currentVertex))
                {
                    path.Insert(0, currentVertex);
                    currentVertex = predecessors[currentVertex];
                }

                if (path.Count > 0)
                {
                    currentWaypointIndex = 0;
                    Debug.Log($"Path found: {string.Join(" -> ", path.Select(v => v.Value))}");
                }
                else
                {
                    Debug.LogWarning("No path found to the player!");
                }
            }
        }
        else
        {
            Debug.LogWarning("Player waypoint ID is -1, cannot update path.");
        }
    }
    private void OnDrawGizmos()
    {
        // Draw Gizmos to visualize the path towards the waypoint
        if (player != null && path.Count > 0)
        {
            for (int i = currentWaypointIndex; i < path.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(path[i].transform.position, 0.2f); // Draw a sphere at each waypoint in the path
            }
        }
    }
}
