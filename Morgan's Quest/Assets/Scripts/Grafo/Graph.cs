using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
   public Dictionary<int,Vertice> Vertices {  get; private set; }
    private void Awake()
    {
        Vertices = new Dictionary<int,Vertice>();
    }
    //value = numero de vertice
    public void AddVertice(int value)
    {
        //Si no contiene el valor crea el nuevo vertice
        if (!Vertices.ContainsKey(value))
        {
            Vertice newVertex = new GameObject($"Vertice{value}").AddComponent<Vertice>();
            newVertex.Initialize(value);
            Vertices[value] = newVertex;
        }
    }
    //numero del origen, numero de destino y el peso
    public void AddArista(int sourceValue, int destinationValue, int weight)
    {
        //Si el vertice tiene origen y destino, le agrega la arista.
        if (Vertices.ContainsKey(sourceValue) && Vertices.ContainsKey(destinationValue))
        {
            Vertices[sourceValue].AddArista(Vertices[destinationValue], weight);
        }
    }
}
