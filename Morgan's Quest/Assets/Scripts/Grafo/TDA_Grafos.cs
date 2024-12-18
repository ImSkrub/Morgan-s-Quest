using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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
            // Crear la arista lógica
            Arista nuevaArista = new Arista(origen, destino, peso);
            aristas.Add(nuevaArista);
            origen.AgregarArista(destino, peso);

            Debug.Log($"Arista agregada entre {origen.iD} y {destino.iD} con peso {peso}");
        }
    }

    public void EliminarArista(Waypoint origen, Waypoint destino)
    {
        Arista aristaAEliminar = aristas.FirstOrDefault(arista =>
            (arista.source == origen && arista.destination == destino) ||
            (arista.source == destino && arista.destination == origen));

        if (aristaAEliminar != null)
        {
            aristas.Remove(aristaAEliminar);
            Debug.Log($"Arista eliminada entre {origen.iD} y {destino.iD}");
        }
    }

    public bool ExisteArista(Waypoint origen, Waypoint destino)
    {
        return aristas.Any(arista =>
            (arista.source == origen && arista.destination == destino) ||
            (arista.source == destino && arista.destination == origen));
    }

    public int PesoArista(Waypoint origen, Waypoint destino)
    {
        var arista = aristas.FirstOrDefault(a =>
            (a.source == origen && a.destination == destino) ||
            (a.source == destino && a.destination == origen));

        return arista != null ? arista.weight : 0;
    }

    public List<Waypoint> GetNodos()
    {
        return nodos;
    }

    public List<Arista> GetAristas()
    {
        return aristas;
    }

    // Método para eliminar aristas basadas en la distancia
    public void EliminarAristasPorDistancia(float distanciaMaxima)
    {
        var aristasAEliminar = new List<Arista>();

        foreach (var arista in aristas)
        {
            float distancia = Vector3.Distance(arista.source.transform.position, arista.destination.transform.position);
            if (distancia > distanciaMaxima)
            {
                aristasAEliminar.Add(arista);
            }
        }

        foreach (var arista in aristasAEliminar)
        {
            EliminarArista(arista.source, arista.destination);
        }
    }
}
