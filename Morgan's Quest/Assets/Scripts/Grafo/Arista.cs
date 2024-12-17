using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arista : MonoBehaviour
{
    public Waypoint source;
    public Waypoint destination;
    public int weight=1;

    public Arista(Waypoint source, Waypoint destination, int weight)
    {
        this.source = source;
        this.destination = destination;
        this.weight = 1;
    }
}
