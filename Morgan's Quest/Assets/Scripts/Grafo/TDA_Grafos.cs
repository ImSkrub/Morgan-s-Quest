using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDA_Grafos : GrafoTDA
{
    static int n = 1;
    public int[,] MAdy;
    public int[,] MId;
    public int[] Etiqs;
    public int cantNodos;

    public void InicializarGrafo()
    {
        MAdy = new int[n, n];
        MId = new int[n, n];
        Etiqs = new int[n];
        cantNodos = 0;
    }

    public void AgregarVertice(int v)
    {
        // Verificar si necesitamos redimensionar los arreglos
        if (cantNodos >= n)
        {
            ExpandirArreglos();
        }

        Etiqs[cantNodos] = v;
        for (int i = 0; i <= cantNodos; i++)
        {
            MAdy[cantNodos, i] = 0;
            MAdy[i, cantNodos] = 0;
        }
        cantNodos++;
    }

    private void ExpandirArreglos()
    {
        int nuevoTamaño = n * 2; // Aumentamos el tamaño al doble
        Debug.Log($"Redimensionando arreglos a tamaño {nuevoTamaño}.");

        // Crear nuevos arreglos con mayor tamaño
        int[,] nuevaMAdy = new int[nuevoTamaño, nuevoTamaño];
        int[,] nuevaMId = new int[nuevoTamaño, nuevoTamaño];
        int[] nuevosEtiqs = new int[nuevoTamaño];

        // Copiar valores existentes a los nuevos arreglos
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                nuevaMAdy[i, j] = MAdy[i, j];
                nuevaMId[i, j] = MId[i, j];
            }
            nuevosEtiqs[i] = Etiqs[i];
        }

        // Reasignar referencias a los nuevos arreglos
        MAdy = nuevaMAdy;
        MId = nuevaMId;
        Etiqs = nuevosEtiqs;

        // Actualizar el tamaño de `n`
        n = nuevoTamaño;
    }

    public void EliminarVertice(int v)
    {
        int ind = Vert2Indice(v);

        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[k, ind] = MAdy[k, cantNodos - 1];
        }

        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[ind, k] = MAdy[cantNodos - 1, k];
        }

        Etiqs[ind] = Etiqs[cantNodos - 1];
        cantNodos--;
    }

    public int Vert2Indice(int v)
    {
        int i = cantNodos - 1;
        while (i >= 0 && Etiqs[i] != v)
        {
            i--;
        }
        return i;
    }

    public void AgregarArista(int id, int v1, int v2, int peso)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        MAdy[o, d] = peso;
        MId[o, d] = id;
    }

    public void EliminarArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        MAdy[o, d] = 0;
        MId[o, d] = 0;
    }

    public bool ExisteArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        return MAdy[o, d] != 0;
    }

    public int PesoArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        return MAdy[o, d];
    }
}
