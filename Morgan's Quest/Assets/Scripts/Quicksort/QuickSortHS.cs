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
        // Verificar si el puntaje ya ha sido agregado recientemente
        if (!puntajes.Contains(score))
        {
            // Asegurarse de que PointManager esté en la escena y que agregue el puntaje
            PointManager.Instance.AddScore(score);

            // Agregar el puntaje a la lista
            puntajes.Add(score);

            // Guardar los puntajes actualizados
            GuardarPuntajes();

            // Actualizar la interfaz de usuario de los puntajes
            ActualizarPuntajes();
        }
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

        OrdenarPuntajes(); // Ordena los puntajes después de cargar
        MostrarPuntajes(); // Actualiza la UI después de ordenar
    }

    private void ActualizarPuntajes()
    {
        // Ordenar los puntajes antes de mostrarlos
        OrdenarPuntajes();

        // Mostrar la lista de puntajes actualizada
        MostrarPuntajes();
    }

    public void OrdenarPuntajes()
    {
        // Usar QuickSort para ordenar los puntajes de mayor a menor
        QuickSort.Sort(puntajes);
    }

    public void MostrarPuntajes()
    {
        string puntajesTexto = "Leaderboard\n";

        for (int i = 0; i < puntajes.Count; i++)
        {
            puntajesTexto += $"{i + 1} Place: - Points: {puntajes[i]}\n";
        }

        globalHighScore.text = puntajesTexto; // Actualizar la UI de la leaderboard
        Debug.Log(puntajesTexto); // Debug output to verify leaderboard display
    }

    private void GuardarPuntajes()
    {
        // Guardar los puntajes en PlayerPrefs
        PlayerPrefs.SetInt("ScoreCount", puntajes.Count); // Guardar la cantidad de puntajes

        for (int i = 0; i < puntajes.Count; i++)
        {
            PlayerPrefs.SetInt($"Score_{i}", puntajes[i]); // Guardar cada puntaje
        }

        PlayerPrefs.Save(); // Asegurarse de que los cambios se guarden
    }
}