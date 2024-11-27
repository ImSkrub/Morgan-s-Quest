using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickSortHS : MonoBehaviour
{
    public TextMeshProUGUI globalHighScore; // Where scores are displayed
    private List<int> puntajes = new List<int>(); // List of scores

    private void Start()
    {
        // Load scores when the game starts
        CargarPuntajes();
        MostrarPuntajes();
        // Subscribe to the score update event
        PointManager.Instance.OnHighScoreUpdated += ActualizarPuntajes;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when this object is destroyed
        PointManager.Instance.OnHighScoreUpdated -= ActualizarPuntajes;
    }

    public void AgregarPuntaje(int score)
    {
        // Ensure there is a PointManager in the scene
        PointManager.Instance.AddScore(score);
        // Add the score to the list
        puntajes.Add(score);
        // Update the displayed scores
        ActualizarPuntajes();
    }

    public void CargarPuntajes()
    {
        puntajes.Clear(); // Clear the existing scores
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0); // Get the number of scores saved

        for (int i = 0; i < scoreCount; i++)
        {
            int score = PlayerPrefs.GetInt($"Score_{i}"); // Load each score
            puntajes.Add(score); // Add it to the list
        }

        // After loading, update the displayed scores
        ActualizarPuntajes();
    }

    private void ActualizarPuntajes()
    {
        Debug.Log("Scores before sorting: " + string.Join(", ", puntajes)); // Debug output
        OrdenarPuntajes();
        Debug.Log("Scores after sorting: " + string.Join(", ", puntajes)); // Debug output
        MostrarPuntajes(); // Ensure this is called after sorting
    }

    public void OrdenarPuntajes()
    {
        QuickSort.Sort(puntajes); // Use QuickSort to sort the scores
    }

    public void MostrarPuntajes()
    {
        string puntajesTexto = "Leaderboard\n";

        for (int i = 0; i < puntajes.Count; i++)
        {
            puntajesTexto += $"{i + 1} Place: - Points: {puntajes[i]}\n";
        }

        globalHighScore.text = puntajesTexto; // Ensure this object is linked
        Debug.Log(puntajesTexto); // Debug output to verify leaderboard display
    }
}