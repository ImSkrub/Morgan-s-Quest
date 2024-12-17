using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arista : MonoBehaviour
{
    public Waypoint source { get; }
    public Waypoint destination { get; }
    public int weight { get; }

    public Arista(Waypoint source, Waypoint destination, int weight)
    {
        this.source = source;
        this.destination = destination;
        this.weight = weight;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(source.gameObject.transform.position,destination.gameObject.transform.position);
    }

}
