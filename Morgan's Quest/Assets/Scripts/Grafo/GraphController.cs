using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    private TDA_Grafos grafo;
    public Waypoint[] nodos; // Array de waypoints
    public Dictionary<int, Waypoint> waypointInScene;
    public float maxDistanciaConexiones = 5f;

    public GameObject aristaPrefab; // Prefab de la arista
    public PathVisualizer pathVisualizer; // Referencia al PathVisualizer


    private void Awake()
    {
        nodos = FindObjectsOfType<Waypoint>();
        InitializeGraph();
    }

    private void InitializeGraph()
    {
        grafo = new TDA_Grafos(aristaPrefab); // Pasamos el prefab al grafo
        waypointInScene = new Dictionary<int, Waypoint>();

        if (nodos == null || nodos.Length == 0)
        {
            Debug.LogError("No se encontraron nodos en la escena.");
            return;
        }

        foreach (Waypoint nodo in nodos)
        {
            if (nodo != null)
            {
                grafo.AgregarVertice(nodo);
                waypointInScene[nodo.iD] = nodo;
            }
        }

        GenerarConexionesAutomaticas();
        // Llamar a PathVisualizer para renderizar las conexiones
        if (pathVisualizer != null)
        {
            pathVisualizer.RenderizarConexiones(grafo);
        }
        else
        {
            Debug.LogError("PathVisualizer no está asignado en el GraphController.");
        }
        Debug.Log(grafo.GetAristas());
    }

    private void GenerarConexionesAutomaticas()
    {
        int conexionesGeneradas = 0;

        foreach (var nodoA in waypointInScene.Values)
        {
            foreach (var nodoB in waypointInScene.Values)
            {
                if (nodoA == nodoB) continue;

                float distancia = Vector3.Distance(nodoA.transform.position, nodoB.transform.position);

                if (distancia <= maxDistanciaConexiones)
                {
                    if (!grafo.ExisteArista(nodoA, nodoB))
                    {
                        grafo.AgregarArista(nodoA, nodoB, Mathf.RoundToInt(distancia));
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
