using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickSortHS : MonoBehaviour
{
    public TextMeshProUGUI globalHighScore; // Donde se muestran los puntajes
    private List<int> puntajes = new List<int>(); // Lista de puntajes

    private void Start()
    {

        AgregarPuntaje(50);
        AgregarPuntaje(30);
        AgregarPuntaje(70);
        CargarPuntajes(); // Cargar puntajes previos al inicio
        MostrarPuntajes();
    }

    public void AgregarPuntaje(int score)
    {
        puntajes.Add(score); // Añade el nuevo puntaje a la lista
        GuardarPuntajes();
    }

    public void OrdenarPuntajes()
    {
        puntajes.Sort((a, b) => b.CompareTo(a)); // Ordenar de mayor a menor
    }

    public void MostrarPuntajes()
    {
        string puntajesTexto = "Leaderboard\n";

        for (int i = 0; i < puntajes.Count; i++)
        {
            puntajesTexto += $"{i + 1} Lugar: - Puntos: {puntajes[i]}\n";
        }

        globalHighScore.text = puntajesTexto; // Asegúrate de que este objeto esté vinculado
    }

    public void GuardarPuntajes()
    {
        for (int i = 0; i < puntajes.Count; i++)
        {
            PlayerPrefs.SetInt($"Score_{i}", puntajes[i]);
        }
        PlayerPrefs.SetInt("ScoreCount", puntajes.Count);
        PlayerPrefs.Save();
    }

    public void CargarPuntajes()
    {
        puntajes.Clear(); // Limpia la lista antes de cargar
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);

        for (int i = 0; i < scoreCount; i++)
        {
            int score = PlayerPrefs.GetInt($"Score_{i}", 0);
            puntajes.Add(score);
        }
    }
}