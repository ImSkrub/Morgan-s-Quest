using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arista : MonoBehaviour
{
    public Vertice Source { get; }
    public Vertice Destination { get; }
    public int Weight { get; }

    public Arista(Vertice source, Vertice destination, int weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }

}
