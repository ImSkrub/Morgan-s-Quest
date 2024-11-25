using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;


public class PointManager : MonoBehaviour
{
    [SerializeField] private Estadisticas estadisticas; // Reference to the Estadisticas class
    private Stack<int> scoreHistory = new Stack<int>();
    private static PointManager instance;

    public static PointManager Instance
    {
        get { return instance; }
    }

    private int highScore = 0;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI highScoreText;

    // Event to notify score changes
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

    public Stack<int> GetScoreHistory()
    {
        return scoreHistory;
    }

    public void SaveFinalScore()
    {
        scoreHistory.Push(estadisticas.puntos);
        NotifyHighScoreUpdated();
    }

    public void AddScore(int points)
    {
        highScore += points;
        scoreHistory.Push(highScore); // Add the score to history
        UpdateHighScoreUI();
        NotifyHighScoreUpdated();
        Debug.Log("Highscore updated: " + highScore);
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "Highscore: " + highScore;
        }
    }

    private void NotifyHighScoreUpdated()
    {
        OnHighScoreUpdated?.Invoke(); // Notify subscribers of the score change
    }
}