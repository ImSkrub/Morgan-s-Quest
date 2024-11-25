using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;


public class PointManager : MonoBehaviour
{
    [SerializeField] public Estadisticas estadisticas;

    private Stack<int> scoreHistory = new Stack<int>();
    private static PointManager instance;

    public static PointManager Instance
    {
        get { return instance; }
    }

    private int highScore = 0;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI highScoreText;

    // Evento para notificar cambios de puntajes
    public event Action OnHighScoreUpdated;

    private void Awake()
    {
        if (PointManager.Instance == null)
        {
            PointManager.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Stack<int> GetScoreHistory()
    {
        return scoreHistory;
    }

    public void SaveFinalScore()
    {
        scoreHistory.Push(estadisticas.puntos);
        OnHighScoreUpdated?.Invoke(); // Notifica cambio
    }

    public void AddScore(int points)
    {
        highScore += points;
        scoreHistory.Push(highScore); // Agrega el puntaje al historial
        UpdateHighScoreUI();
        OnHighScoreUpdated?.Invoke(); // Notifica cambio
        Debug.Log("Highscore actualizado: " + highScore);
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "Highscore: " + highScore;
        }
    }
}