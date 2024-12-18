using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleAvoidance : IObstacleAvoidance
{
    private LayerMask wallLayer;
    private GraphController controller;
    public ObstacleAvoidance(LayerMask wallLayer,GraphController graphController)
    {
        this.wallLayer = wallLayer;
        this.controller = graphController;
    }

    public Vector3 GetAdjustedDirection(Transform enemyTransform, Vector3 targetDirection, float rayDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(enemyTransform.position, targetDirection, rayDistance, wallLayer);

        if (hit.collider != null)
        {
            // Encuentra el waypoint más cercano que no esté bloqueado
            
            Waypoint nearestWaypoint = controller.GetGraph().GetNodos()
                .OrderBy(wp => Vector3.Distance(enemyTransform.position, wp.transform.position))
                .FirstOrDefault(wp => !Physics2D.Raycast(enemyTransform.position, wp.transform.position - enemyTransform.position, rayDistance, wallLayer));

            if (nearestWaypoint != null)
            {
                return (nearestWaypoint.transform.position - enemyTransform.position).normalized;
            }
        }

        return targetDirection;
    }
}
