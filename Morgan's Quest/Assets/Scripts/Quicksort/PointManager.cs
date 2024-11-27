using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;


public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }
    private Stack<int> scoreHistory = new Stack<int>();
    public int CurrentScore { get; private set; } // Puntaje actual

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        CurrentScore += score; // Sumar al puntaje actual
        scoreHistory.Push(CurrentScore); // Guardar el puntaje en el historial
        OnHighScoreUpdated?.Invoke();
    }

    public void SaveScore()
    {
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);
        PlayerPrefs.SetInt($"Score_{scoreCount}", CurrentScore); // Guardar el puntaje actual
        PlayerPrefs.SetInt("ScoreCount", scoreCount + 1); // Incrementar el contador de puntajes
        PlayerPrefs.Save(); // Guardar cambios en PlayerPrefs
        Debug.Log($"Puntaje guardado: {CurrentScore}");
    }

    public Stack<int> GetScoreHistory()
    {
        return scoreHistory;
    }

    public event System.Action OnHighScoreUpdated;

    public void ResetCurrentScore()
    {
        CurrentScore = 0; // Reiniciar el puntaje actual
    }
}