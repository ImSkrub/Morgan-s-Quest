using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    private TDA_Grafos grafo;
    public Dictionary<int, Waypoint> waypointInScene;
    public Waypoint[] nodos;
    public float maxDistanciaConexiones = 5f;
    private void Awake()
    {
        InitializeGraph();
    }
    private void InitializeGraph()
    {
        grafo = new TDA_Grafos();
        grafo.InicializarGrafo();
        waypointInScene = new Dictionary<int, Waypoint>();
        
        if (nodos.Length == 0)
        {
            Debug.LogError("No se encontraron nodos!");
            return;
        }

        foreach (Waypoint nodo in nodos)
        {
            grafo.AgregarVertice(nodo.waypointId);
            waypointInScene.Add(nodo.waypointId, nodo);
        }

        GenerarConexionesAutomaticas();

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
                    grafo.AgregarArista(idArista++, nodoA.Key, nodoB.Key, ((int)distancia));
                    grafo.AgregarArista(idArista++, nodoB.Key, nodoA.Key, ((int)distancia));
                }
            }
        }

        Debug.Log("Conexiones autom�ticas generadas.");
    }
    /*
    public Vector3[] ObtenerPosicionesCamino(Vector3 inicio, Vector3 fin)
    {
        int nodoInicial = ObtenerNodoMasCercano(inicio);
        int nodoFinal = ObtenerNodoMasCercano(fin);

        AlgDijkstra.Dijkstra(grafo, nodoInicial);

        int nodoFinalIndex = grafo.Vert2Indice(nodoFinal);
        string caminoStr = AlgDijkstra.nodos[nodoFinalIndex];

        if (string.IsNullOrEmpty(caminoStr))
            return null;

        string[] nodosIds = caminoStr.Split(',');
        Vector3[] posiciones = new Vector3[nodosIds.Length];

        for (int i = 0; i < nodosIds.Length; i++)
        {
            int id = int.Parse(nodosIds[i]);
            posiciones[i] = waypointInScene[id].transform.position;
        }

        return posiciones;
    }

    private int ObtenerNodoMasCercano(Vector3 posicion)
    {
        float distanciaMinima = float.MaxValue;
        int idNodoMasCercano = 1;

        foreach (var nodo in waypointInScene)
        {
            float distancia = Vector2.Distance(
                new Vector2(posicion.x, posicion.y),
                new Vector2(nodo.Value.transform.position.x, nodo.Value.transform.position.y)
            );
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                idNodoMasCercano = nodo.Key;
            }
        }
        return idNodoMasCercano;
    }*/
    public TDA_Grafos GetGraph()
    {
        return grafo;
    }
}
