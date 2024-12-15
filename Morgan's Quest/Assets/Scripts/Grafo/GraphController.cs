using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    private TDA_Grafos grafo;
    public Dictionary<int, Waypoint> waypointInScene;
    public Waypoint[] nodos; // Array de nodos que se configuran manualmente en Unity
    public float maxDistanciaConexiones = 5f; // Distancia máxima para conectar nodos

    private void Awake()
    {
        InitializeGraph(); // Inicializa el grafo y genera conexiones
    }

    private void InitializeGraph()
    {
        grafo = new TDA_Grafos();
        grafo.InicializarGrafo();
        waypointInScene = new Dictionary<int, Waypoint>();

        if (nodos.Length == 0)
        {
            Debug.LogError("No se encontraron nodos en la escena. Asegúrate de asignarlos en el inspector.");
            return;
        }

        // Agregar cada nodo al grafo y al diccionario de waypoints
        foreach (Waypoint nodo in nodos)
        {
            grafo.AgregarVertice(nodo.waypointId);
            waypointInScene.Add(nodo.waypointId, nodo);
        }

        GenerarConexionesAutomaticas(); // Genera las conexiones entre nodos automáticamente
    }

    private void GenerarConexionesAutomaticas()
    {
        int idArista = 1;

        foreach (var nodoA in waypointInScene)
        {
            foreach (var nodoB in waypointInScene)
            {
                // Evitar conectar un nodo consigo mismo
                if (nodoA.Key == nodoB.Key)
                    continue;

                // Calcular la distancia entre los nodos
                float distancia = Vector3.Distance(
                    nodoA.Value.transform.position,
                    nodoB.Value.transform.position
                );

                // Si la distancia es menor o igual al umbral, conectar los nodos
                if (distancia <= maxDistanciaConexiones)
                {
                    grafo.AgregarArista(idArista++, nodoA.Key, nodoB.Key, Mathf.RoundToInt(distancia));
                    grafo.AgregarArista(idArista++, nodoB.Key, nodoA.Key, Mathf.RoundToInt(distancia));
                }
            }
        }

        Debug.Log("Conexiones automáticas generadas entre los waypoints.");
    }

    public TDA_Grafos GetGraph()
    {
        return grafo;
    }
}
