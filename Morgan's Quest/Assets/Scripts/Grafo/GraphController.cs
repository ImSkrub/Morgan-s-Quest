using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    private TDA_Grafos grafo;
    public Waypoint[] nodos; // Array de waypoints que se agarran
    public Dictionary<int, Waypoint> waypointInScene;
    public float maxDistanciaConexiones = 5f; // Distancia máxima para conectar nodos

    private void Awake()
    {
        nodos = FindObjectsOfType<Waypoint>();
        InitializeGraph(); // Inicializa el grafo y genera conexiones
    }

    private void InitializeGraph()
    {
        grafo = new TDA_Grafos();
        waypointInScene = new Dictionary<int, Waypoint>();

        if (nodos == null || nodos.Length == 0)
        {
            Debug.LogError("No se encontraron nodos en la escena. Asegúrate de asignarlos en el inspector.");
            return;
        }

        // Agregar cada nodo al grafo y al diccionario
        foreach (Waypoint nodo in nodos)
        {
            if (nodo != null)
            {
                grafo.AgregarVertice(nodo);
                waypointInScene[nodo.iD] = nodo;
            }
        }

        // Generar conexiones automáticas entre los waypoints
        GenerarConexionesAutomaticas();
    }

    private void GenerarConexionesAutomaticas()
    {
        int conexionesGeneradas = 0;  // Contador de conexiones generadas

        foreach (var nodoA in waypointInScene.Values)
        {
            foreach (var nodoB in waypointInScene.Values)
            {
                if (nodoA == nodoB) continue;  // No conectar el nodo consigo mismo

                float distancia = Vector3.Distance(nodoA.transform.position, nodoB.transform.position);

                if (distancia <= maxDistanciaConexiones)
                {
                    // Asegurarse de que no haya arista existente
                    if (!grafo.ExisteArista(nodoA, nodoB))
                    {
                        grafo.AgregarArista(nodoA, nodoB, Mathf.RoundToInt(distancia));
                        Debug.Log($"Conexión creada entre Waypoint {nodoA.iD} y Waypoint {nodoB.iD} con peso {Mathf.RoundToInt(distancia)}");
                        conexionesGeneradas++;
                    }
                }
            }
        }

        Debug.Log($"Conexiones automáticas generadas: {conexionesGeneradas}");
    }

    public TDA_Grafos GetGraph()
    {
        return grafo;
    }

}
