using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;


public class PointManager : MonoBehaviour
{
    private Stack<int> scoreHistory = new Stack<int>();
    private static PointManager instance;

    public static PointManager Instance => instance;

    private int highScore = 0;

      
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
        return new Stack<int>(scoreHistory); 
    }

    
    public void SaveFinalScore(int finalScore)
    {
        scoreHistory.Push(finalScore);
        NotifyHighScoreUpdated();
    }

   
    public void AddScore(int points)
    {
        highScore += points;
        scoreHistory.Push(highScore); 
        NotifyHighScoreUpdated();
        Debug.Log("Highscore actualizado: " + highScore);
    }

   
    public void UpdateHighScoreUI(TMP_Text highScoreText)
    {
        if (highScoreText != null)
        {
            highScoreText.text = $"Highscore: {highScore}";
        }
        else
        {
            Debug.LogWarning("No se asignó un TextMeshProUGUI al PointManager.");
        }
    }

    
    private void NotifyHighScoreUpdated()
    {
        if (OnHighScoreUpdated != null)
        {
            OnHighScoreUpdated?.Invoke();
        }
    }
}