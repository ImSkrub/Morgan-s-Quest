using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDA_Grafos
{
    private List<Waypoint> nodos;
    private List<Arista> aristas;

    private GameObject aristaPrefab; // Prefab de arista

    public TDA_Grafos(GameObject aristaPrefab)
    {
        nodos = new List<Waypoint>();
        aristas = new List<Arista>();
        this.aristaPrefab = aristaPrefab; // Guardamos la referencia al prefab
    }

    public void AgregarVertice(Waypoint v)
    {
        if (!nodos.Contains(v))
        {
            nodos.Add(v);
            Debug.Log($"Vertice agregado: {v.iD}");
        }
    }

    public void AgregarArista(Waypoint origen, Waypoint destino, int peso)
    {
        if (!ExisteArista(origen, destino))
        {
            // Crear la arista l�gica
            Arista nuevaArista = new Arista(origen, destino, peso);
            aristas.Add(nuevaArista);
            origen.AgregarArista(destino, peso);

            Debug.Log($"Arista agregada entre {origen.iD} y {destino.iD} con peso {peso}");
        }
    }


    public bool ExisteArista(Waypoint origen, Waypoint destino)
    {
        foreach (Arista arista in aristas)
        {
            if ((arista.source == origen && arista.destination == destino) ||
                (arista.source == destino && arista.destination == origen))
            {
                return true;
            }
        }
        return false;
    }

    public int PesoArista(Waypoint origen, Waypoint destino)
    {
        foreach (Arista arista in aristas)
        {
            if ((arista.source == origen && arista.destination == destino) ||
                (arista.source == destino && arista.destination == origen))
            {
                return arista.weight;
            }
        }
        return 0;
    }

    public List<Waypoint> GetNodos()
    {
        return nodos;
    }

    public List<Arista> GetAristas()
    {
        return aristas;
    }
}
