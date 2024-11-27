using System;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort
{
    public static void Sort(List<int> list)
    {
        if (list == null || list.Count <= 1)
            return;

        QuickSortAlgorithm(list, 0, list.Count - 1);
    }

    private static void QuickSortAlgorithm(List<int> list, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(list, left, right);
            QuickSortAlgorithm(list, left, pivotIndex - 1);
            QuickSortAlgorithm(list, pivotIndex + 1, right);
        }
    }

    private static int Partition(List<int> list, int left, int right)
    {
        int pivot = list[right]; // Elegir el último elemento como pivote
        int i = left - 1; // Índice del elemento más pequeño

        for (int j = left; j < right; j++)
        {
            if (list[j] > pivot) // Cambiado a '>' para ordenar de mayor a menor
            {
                i++;
                Swap(list, i, j); // Intercambiar elementos
            }
        }

        // Colocar el pivote en su posición correcta
        Swap(list, i + 1, right);
        return i + 1; // Devolver el índice del pivote
    }

    private static void Swap(List<int> list, int i, int j)
    {
        int temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
