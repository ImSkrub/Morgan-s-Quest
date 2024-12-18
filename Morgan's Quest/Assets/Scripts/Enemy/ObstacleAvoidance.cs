using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleAvoidance : IObstacleAvoidance
{
    private LayerMask wallLayer;
    private GraphController controller;

    public ObstacleAvoidance(LayerMask wallLayer, GraphController graphController)
    {
        this.wallLayer = wallLayer;
        this.controller = graphController;
    }

    public Vector3 GetAdjustedDirection(Transform enemyTransform, Vector3 targetDirection, float rayDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(enemyTransform.position, targetDirection, rayDistance, wallLayer);

        if (hit.collider != null)
        {
            // Si se detecta un obstáculo, buscamos el waypoint más cercano sin bloqueo
            foreach (var waypoint in controller.GetGraph().GetNodos())
            {
                // Verificamos si el waypoint está accesible
                Vector3 directionToWaypoint = waypoint.transform.position - enemyTransform.position;
                RaycastHit2D waypointHit = Physics2D.Raycast(enemyTransform.position, directionToWaypoint, rayDistance, wallLayer);

                if (waypointHit.collider == null) 
                {
                    return directionToWaypoint.normalized;
                }
            }

            // Si no se encontró ningún waypoint accesible, podríamos retornar una dirección aleatoria
            return Random.insideUnitCircle.normalized;
        }

        // Si no hay obstáculos, seguimos la dirección original
        return targetDirection;
    }
}
