using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickSortHS : MonoBehaviour
{
    public TextMeshProUGUI globalHighScore;
    private PointManager pointManager;
    private List<int> puntajes;

    private void Start()
    {
        pointManager = PointManager.Instance;

        // Se suscribe al evento de actualización del highscore
        pointManager.OnHighScoreUpdated += ActualizarHighScores;

        // Se actualizan los high scores en cuanto inicie
        ActualizarHighScores();
    }

    private void OnDestroy()
    {
        // Se desuscribe al evento cuando el objeto se destruya
        if (pointManager != null)
        {
            pointManager.OnHighScoreUpdated -= ActualizarHighScores;
        }
    }

    private void ActualizarHighScores()
    {
        GenerarPuntajes();
        OrdenarPuntajes();
        MostrarPuntajes();
    }

    private void GenerarPuntajes()
    {
        // Se genera una lista con los puntajes del Stack
        puntajes = new List<int>(pointManager.GetScoreHistory());
    }

    private void OrdenarPuntajes()
    {
        // Ordena la lista de puntajes usando QuickSort
        QuickSort(puntajes, 0, puntajes.Count - 1);
    }

    private void MostrarPuntajes()
    {
        string puntajesTexto = "";

        // Muestra los puntajes ordenados en el TextMeshProUGUI
        for (int i = 0; i < puntajes.Count; i++)
        {
            puntajesTexto += (i + 1) + " Lugar: " + puntajes[i] + "\n";
        }

        globalHighScore.text = puntajesTexto;
    }

    private void QuickSort(List<int> list, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(list, left, right);
            QuickSort(list, left, pivotIndex - 1);
            QuickSort(list, pivotIndex + 1, right);
        }
    }

    private int Partition(List<int> list, int left, int right)
    {
        int pivotValue = list[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            // Compara y organiza los puntajes de mayor a menor
            if (list[j] > pivotValue)
            {
                i++;
                Swap(list, i, j);
            }
        }

        Swap(list, i + 1, right);
        return i + 1;
    }

    private void Swap(List<int> list, int i, int j)
    {
        int temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}