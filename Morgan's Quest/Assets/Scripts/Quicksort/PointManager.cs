using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;


public class PointManager : MonoBehaviour
{
    private static PointManager instance;

    public static PointManager Instance => instance;

    private int highScore = 0;  // Puntaje m�s alto

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI highScoreText;  // Para mostrar el puntaje m�s alto en la UI

    public event Action OnHighScoreUpdated;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetHighScore()
    {
        return highScore; // Devuelve el puntaje m�s alto acumulado
    }

    // Guardar puntaje final
    public void SaveFinalScore(int finalScore)
    {
        highScore = finalScore;
        NotifyHighScoreUpdated();
    }

    // A�adir puntaje y actualizar el puntaje m�s alto
    public void AddScore(int points)
    {
        highScore += points;
        UpdateHighScoreUI();
        NotifyHighScoreUpdated();
        Debug.Log("Highscore actualizado: " + highScore);
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = $"Highscore: {highScore}"; // Actualiza la UI
        }
        else
        {
            Debug.LogWarning("No se asign� un TextMeshProUGUI al PointManager.");
        }
    }

    private void NotifyHighScoreUpdated()
    {
        OnHighScoreUpdated?.Invoke();  // Notifica que el puntaje ha sido actualizado
    }
}