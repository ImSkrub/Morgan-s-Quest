using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    public GameObject linePrefab; // Prefab con LineRenderer
    private List<GameObject> lineObjects = new List<GameObject>();
    public bool debug = false;

    public void RenderizarConexiones(TDA_Grafos grafo)
    {
        LimpiarLineas(); // Limpiar líneas anteriores

        List<Arista> aristas = grafo.GetAristas();
        if (debug)
        {
            foreach (Arista arista in aristas)
            {
                // Crear un objeto de línea
                GameObject nuevaLinea = Instantiate(linePrefab, transform);
                LineRenderer lineRenderer = nuevaLinea.GetComponent<LineRenderer>();

                if (lineRenderer != null)
                {
                    // Configurar el material dinámicamente
                    Material lineMaterial = new Material(Shader.Find("Unlit/Color"));
                    lineMaterial.color = Color.green; // Cambia el color aquí
                    lineRenderer.material = lineMaterial;

                    // Configurar el LineRenderer para 2D
                    lineRenderer.positionCount = 2;
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;

                    // Asignar las posiciones en el mismo plano 2D (Z = 0)
                    Vector3 posOrigen = arista.source.transform.position;
                    Vector3 posDestino = arista.destination.transform.position;

                    posOrigen.z = 0;
                    posDestino.z = 0;

                    lineRenderer.SetPosition(0, posOrigen);
                    lineRenderer.SetPosition(1, posDestino);

                    // Configurar Sorting Layer para que se vea en 2D
                    lineRenderer.sortingLayerName = "Foreground";
                    lineRenderer.sortingOrder = 1;
                }

                lineObjects.Add(nuevaLinea);
            }
        }
    }

    public void LimpiarLineas()
    {
        // Destruir todas las líneas anteriores
        foreach (GameObject linea in lineObjects)
        {
            Destroy(linea);
        }
        lineObjects.Clear();
    }
}
