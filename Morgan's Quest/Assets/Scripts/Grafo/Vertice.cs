using System.Collections.Generic;
using UnityEngine;

public class Vertice : MonoBehaviour
{
    public int Value { get; private set; }
    public List<Arista> aristas { get; private set; }

    public Vector2 Position => transform.position;

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
