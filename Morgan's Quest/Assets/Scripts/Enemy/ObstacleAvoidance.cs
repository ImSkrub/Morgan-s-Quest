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
        Debug.DrawRay(enemyTransform.position, targetDirection * rayDistance, hit.collider != null ? Color.red : Color.green);

        if (hit.collider != null)
        {
            // Intentar encontrar un waypoint accesible
            Vector3 adjustedDirection = BuscarWaypointAccesible(enemyTransform, rayDistance);
            if (adjustedDirection != Vector3.zero)
            {
                return adjustedDirection; // Ir hacia un waypoint accesible
            }

            // Intentar evitar el obstáculo
            Vector3 avoidanceDirection = EvitarObstaculo(enemyTransform, targetDirection, rayDistance);
            if (avoidanceDirection != Vector3.zero)
            {
                return avoidanceDirection; // Evitar el obstáculo
            }

            // Si no podemos evitar el obstáculo, recalculamos el camino usando Dijkstra
            EnemyFollow enemyFollow = enemyTransform.GetComponent<EnemyFollow>();
            if (enemyFollow != null)
            {
                enemyFollow.ActualizarCamino(); // Recalcular el camino usando Dijkstra
                if (enemyFollow.camino.Count > 0)
                {
                    return (enemyFollow.camino[0].transform.position - enemyTransform.position).normalized;
                }
            }

            // Si no hay camino alternativo, detenerse
            return Vector3.zero;
        }

        return targetDirection; // Continuar en la dirección actual
    }

    private Vector3 BuscarWaypointAccesible(Transform enemyTransform, float maxRayDistance)
    {
        Waypoint waypointAccesible = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (var waypoint in controller.GetGraph().GetNodos())
        {
            Vector3 directionToWaypoint = (waypoint.transform.position - enemyTransform.position).normalized;
            float distanciaWaypoint = Vector3.Distance(enemyTransform.position, waypoint.transform.position);

            // Solo considerar waypoints dentro del rango del rayo
            if (distanciaWaypoint > maxRayDistance) continue;

            // Comprobar si el camino al waypoint está libre
            RaycastHit2D hit = Physics2D.Raycast(enemyTransform.position, directionToWaypoint, distanciaWaypoint, wallLayer);
            Debug.DrawRay(enemyTransform.position, directionToWaypoint * distanciaWaypoint, hit.collider != null ? Color.red : Color.green);

            if (hit.collider == null && distanciaWaypoint < distanciaMinima)
            {
                waypointAccesible = waypoint;
                distanciaMinima = distanciaWaypoint;
            }
        }

        if (waypointAccesible != null)
        {
            return (waypointAccesible.transform.position - enemyTransform.position).normalized;
        }

        return Vector3.zero; // Si no hay waypoints accesibles
    }

    private Vector3 EvitarObstaculo(Transform enemyTransform, Vector3 targetDirection, float rayDistance)
    {
        for (int angle = 15; angle <= 90; angle += 15)
        {
            Vector3 leftDirection = Quaternion.Euler(0, 0, angle) * targetDirection;
            Vector3 rightDirection = Quaternion.Euler(0, 0, -angle) * targetDirection;

            RaycastHit2D leftHit = Physics2D.Raycast(enemyTransform.position, leftDirection, rayDistance, wallLayer);
            RaycastHit2D rightHit = Physics2D.Raycast(enemyTransform.position, rightDirection, rayDistance, wallLayer);

            Debug.DrawRay(enemyTransform.position, leftDirection * rayDistance, leftHit.collider != null ? Color.red : Color.blue);
            Debug.DrawRay(enemyTransform.position, rightDirection * rayDistance, rightHit.collider != null ? Color.red : Color.blue);

            if (leftHit.collider == null)
            {
                return leftDirection;
            }
            if (rightHit.collider == null)
            {
                return rightDirection;
            }
        }

        return Vector3.zero;
    }
}
