using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertice : MonoBehaviour
{
    public int Value { get;set; }
    public List<Arista> aristas { get; private set; }
    public void Initialize(int value)
    {
        Value = value; 
        aristas = new List<Arista>();
    }
    public void AddArista(Vertice destino, int peso)
    {
        aristas.Add(new Arista(this, destino, peso));
    }
}
