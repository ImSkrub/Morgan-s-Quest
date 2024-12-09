using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    private GraphController graphController;
    private List<int> path = new List<int>(); //Todo se maneja en interger
    private int currentWaypointIndex = 0;
    public float avoidanceRayLength = 1.5f;
    
    protected override void Start()
    {
        base.Start();
        graphController = FindObjectOfType<GraphController>();
        if (graphController != null)
        {
            CalculatePathToPlayer();
        }
    }

    private void Update()
    {
        base.Update();

        //Si la lista no esta vacia y el index del waypoint es menor a la cantidad que hay en la lista. Se mueve hacia el waypoint
        if (path.Count > 0 && currentWaypointIndex < path.Count)
        {
            int targetWaypointId = path[currentWaypointIndex];
            Vector3 targetPosition = graphController.waypointInScene[targetWaypointId].transform.position;

            // Move towards the target waypoint
            MoveTowardsWaypoint(targetPosition);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
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
        if (graphController == null || player == null) return;

        int enemyVerticeId = GetClosestVerticeId();
        int playerVerticeId = player.GetComponent<Waypoint>().waypointId;

        AlgDijkstra.Dijkstra(graphController.GetGraph(), enemyVerticeId);

        path.Clear();
        int current = playerVerticeId;
        while (current != -1)
        {
            path.Insert(0, current);
            current = graphController.GetGraph().Vert2Indice(current);
        }

        currentWaypointIndex = 0;
    }

    private int GetClosestVerticeId()
    {
        TDA_Grafos graph = graphController.GetGraph();
        int closestId = -1;
        float minDistance = float.MaxValue;

        foreach (var waypointId in graph.Etiqs)
        {
            Vector3 waypointPosition = graphController.waypointInScene[waypointId].transform.position;
            float distance = Vector3.Distance(transform.position, waypointPosition);
            if (distance < minDistance)
            {
                closestId = waypointId;
                minDistance = distance;
            }
        }

        return closestId;
    }

    private void MoveTowardsWaypoint(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Comprobar si hay obstáculos antes de moverse
        if (!IsObstacleInPath(direction))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, vel * Time.deltaTime);
        }
        else
        {
            AvoidObstacle(direction);
        }
    }

    private bool IsObstacleInPath(Vector3 direction)
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, direction, avoidanceRayLength, wallLayer);
    }

    private void AvoidObstacle(Vector3 direction)
    {
        Vector3 avoidanceDirection = direction;

        // Lanzar un rayo hacia la izquierda
        if (!IsObstacleInPath(Quaternion.Euler(0, -30, 0) * direction))
        {
            avoidanceDirection = Quaternion.Euler(0, -30, 0) * direction;
        }
        // Lanzar un rayo hacia la derecha
        else if (!IsObstacleInPath(Quaternion.Euler(0, 30, 0) * direction))
        {
            avoidanceDirection = Quaternion.Euler(0, 30, 0) * direction;
        }

        transform.position += avoidanceDirection * vel * Time.deltaTime;
    }
}
