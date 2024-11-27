using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;


public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }
    private Stack<int> scoreHistory = new Stack<int>();
    public int CurrentScore { get; private set; } // Current score

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
        CurrentScore += score; // Add to current score
        scoreHistory.Push(CurrentScore); // Save score in history
        OnHighScoreUpdated?.Invoke();
    }

    public void SaveScore()
    {
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);
        PlayerPrefs.SetInt($"Score_{scoreCount}", CurrentScore); // Save current score
        PlayerPrefs.SetInt("ScoreCount", scoreCount + 1); // Increment score count
        PlayerPrefs.Save(); // Save changes in PlayerPrefs
        Debug.Log($"Score saved: {CurrentScore}");
    }

    public Stack<int> GetScoreHistory()
    {
        return scoreHistory;
    }

    public event System.Action OnHighScoreUpdated;

    public void ResetCurrentScore()
    {
        CurrentScore = 0; // Reset current score
    }
}