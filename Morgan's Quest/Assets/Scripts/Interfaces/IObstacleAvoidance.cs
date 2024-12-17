using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleAvoidance
{
    Vector3 GetAdjustedDirection(Transform enemyTransform, Vector3 targetDirection, float rayDistance);
}