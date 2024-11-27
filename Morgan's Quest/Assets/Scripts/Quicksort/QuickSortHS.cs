using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickSortHS : MonoBehaviour
{
    public TextMeshProUGUI globalHighScore; // Donde se muestran los puntajes
    private List<int> puntajes = new List<int>(); // Lista de puntajes

    private void Start()
    {
        CargarPuntajes(); // Cargar puntajes al inicio
        MostrarPuntajes();
        // Suscribirse al evento de actualizaci�n de puntajes
        PointManager.Instance.OnHighScoreUpdated += ActualizarPuntajes;
    }

    private void OnDestroy()
    {
        // Cancelar la suscripci�n al evento cuando este objeto se destruye
        PointManager.Instance.OnHighScoreUpdated -= ActualizarPuntajes;
    }

    public void AgregarPuntaje(int score)
    {
        // Asegurarse de que hay un PointManager en la escena
        PointManager.Instance.AddScore(score);
        // Actualizar los puntajes mostrados
        ActualizarPuntajes();
    }

    private void ActualizarPuntajes()
    {
        puntajes.Clear();
        Stack<int> scoreHistory = PointManager.Instance.GetScoreHistory();
        foreach (var score in scoreHistory)
        {
            puntajes.Add(score);
        }
        OrdenarPuntajes();
        MostrarPuntajes();
    }

    public void OrdenarPuntajes()
    {
        QuickSort.Sort(puntajes); // Usar QuickSort para ordenar los puntajes
    }

    public void MostrarPuntajes()
    {
        string puntajesTexto = "Leaderboard\n";

        for (int i = 0; i < puntajes.Count; i++)
        {
            puntajesTexto += $"{i + 1} Lugar: - Puntos: {puntajes[i]}\n";
        }

        globalHighScore.text = puntajesTexto; // Aseg�rate de que este objeto est� vinculado
        Debug.Log(puntajesTexto); // Salida de depuraci�n para verificar la visualizaci�n del leaderboard
    }

    public void CargarPuntajes()
    {
        puntajes.Clear(); // Limpiar la lista antes de cargar
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);
        Debug.Log($"Cargando {scoreCount} puntajes."); // Salida de depuraci�n para el conteo de puntajes

        for (int i = 0; i < scoreCount; i++)
        {
            int score = PlayerPrefs.GetInt($"Score_{i}", 0);
            puntajes.Add(score);
            Debug.Log($"Puntaje cargado: {score}"); // Salida de depuraci�n para cada puntaje cargado
        }
    }
}