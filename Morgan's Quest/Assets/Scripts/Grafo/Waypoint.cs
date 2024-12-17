using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int iD; // Identificador único
    public List<Arista> aristasConectadas = new List<Arista>();

    public void AgregarArista(Waypoint destino, int peso = 1)
    {
        Arista nuevaArista = new Arista(this, destino, peso);
        aristasConectadas.Add(nuevaArista);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
