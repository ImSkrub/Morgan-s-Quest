using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickSortHS : MonoBehaviour
{
    public TextMeshProUGUI globalHighScore; // Donde se muestran los jugadores
    private PointManager pointManager;
    private List<EscenceScore> puntajes;

    private void Start()
    {
        pointManager = PointManager.Instance;

        // Suscribirse al evento de actualización de highscore
        pointManager.OnHighScoreUpdated += ActualizarHighScores;

        // Cargar y mostrar los puntajes iniciales
        ActualizarHighScores();
    }

    private void Update()
    {
        ActualizarHighScores();
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
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
        puntajes = new List<EscenceScore>();
        Stack<int> scoreHistory = pointManager.GetScoreHistory();

        foreach (int score in scoreHistory)
        {
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
            puntajesTexto += (i + 1) + " Lugar: " + puntajes[i].Puntaje + "\n";
        }

        globalHighScore.text = puntajesTexto;
    }
}