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

    public void GenerarPuntajes()
    {
        puntajes = new List<EscenceScore>();
        
        Stack<int> scoreHistory = pointManager.GetScoreHistory();
        foreach (int score in scoreHistory)
        {
            Debug.Log(score);
            puntajes.Add(new EscenceScore(score));
        }
     
    }

    public void OrdenarPuntajes()
    {
        QuickSort.Sort(puntajes);
    }

    public void MostrarPuntajes()
    {
        string puntajesTexto = "Leaderboard\n";

        for (int i = 0; i < puntajes.Count; i++)
        {
            puntajesTexto += (i + 1) + " Lugar: " + " - Escencias totales: " + puntajes[i].Puntaje + "\n";
        }

        globalHighScore.text = puntajesTexto;
    }
}
