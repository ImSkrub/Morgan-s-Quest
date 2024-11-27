using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickSortHS : MonoBehaviour
{
    public TextMeshProUGUI globalHighScore; // Donde se muestran los puntajes
    private int totalPuntaje = 0; // Para almacenar el puntaje total acumulado

    private void Start()
    {
        CargarPuntajes(); // Cargar puntajes previos al inicio
        MostrarPuntajes();
    }

    public void AgregarPuntaje(int score)
    {
        totalPuntaje += score;  // Agregar puntaje al total
        GuardarPuntajes(totalPuntaje); // Guardar el puntaje total
    }

    public void MostrarPuntajes()
    {
        string puntajesTexto = "Leaderboard\n";
        puntajesTexto += $"Total: {totalPuntaje} puntos\n";
        globalHighScore.text = puntajesTexto; // Asegúrate de que este objeto esté vinculado
    }

    public void GuardarPuntajes(int score)
    {
        PlayerPrefs.SetInt("TotalScore", score);  // Guardar el puntaje total
        PlayerPrefs.Save();
    }

    public void CargarPuntajes()
    {
        totalPuntaje = PlayerPrefs.GetInt("TotalScore", 0); // Cargar el puntaje total acumulado
    }
}