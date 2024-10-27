using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickSortHS : MonoBehaviour
{
    public TextMeshProUGUI globalHighScore; //Donde se muestran los jugadores
    private int cuenta = 0;

    private PointManager pointManager;
    private List<EscenceScore> puntajes;

    private void Start()
    {
        Debug.Log(cuenta);
        pointManager = PointManager.Instance;
        GenerarPuntajes();
        OrdenarPuntajes();
        MostrarPuntajes();
    }

    private void GenerarPuntajes()
    {
        puntajes = new List<EscenceScore>();
        
        Stack<int> scoreHistory = pointManager.GetScoreHistory();
        foreach (int score in scoreHistory)
        {
            Debug.Log(score);
            puntajes.Add(new EscenceScore(score));
        }
     
    }

    private void OrdenarPuntajes()
    {
        QuickSort.Sort(puntajes);
    }

    private void MostrarPuntajes()
    {
        string puntajesTexto = "";

        for (int i = 0; i < puntajes.Count; i++)
        {
            puntajesTexto += (i + 1) + "Lugar: " + " - Escencias totales: " + puntajes[i].Puntaje + "\n";
            Debug.Log((i + 1) + "Lugar: " + " - Escencias totales: " + puntajes[i].Puntaje);
        }

        globalHighScore.text = puntajesTexto;
    }
}
