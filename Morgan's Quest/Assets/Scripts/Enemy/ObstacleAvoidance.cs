using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    private LayerMask wallLayer;

    public ObstacleAvoidance(LayerMask wallLayer)
    {
        this.wallLayer = wallLayer;
    }

    public Vector3 GetAdjustedDirection(Transform enemyTransform, Vector3 targetDirection, float rayDistance)
    {
        // Realiza un Raycast al frente del enemigo
        RaycastHit2D hit = Physics2D.Raycast(enemyTransform.position, targetDirection, rayDistance, wallLayer);

        if (hit.collider != null)
        {
            // Si detecta una pared, calcula una dirección de evasión simple (perpendicular)
            Vector3 perpendicularDirection = Vector3.Cross(targetDirection, Vector3.forward).normalized;
            return perpendicularDirection; // Retorna la dirección ajustada
        }

        return targetDirection; // Si no hay obstáculo, sigue la dirección original
    }
}
