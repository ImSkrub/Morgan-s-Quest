using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;


public class PointManager : MonoBehaviour
{
    private Stack<int> scoreHistory = new Stack<int>();
    private static PointManager instance;

    public static PointManager Instance
    {
        get
        {
            // Verificar si la instancia ya está asignada, si no, buscarla en la escena
            if (instance == null)
            {
                instance = FindObjectOfType<PointManager>();
                if (instance == null)
                {
                    Debug.LogError("No se encontró un PointManager en la escena. Asegúrate de que esté presente.");
                }
            }
            return instance;
        }
    }

    private int highScore = 0;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI highScoreText;

    public event Action OnHighScoreUpdated;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Esto asegura que el objeto no se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruimos la nueva
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
        UpdateHighScoreUI();
        NotifyHighScoreUpdated();
        Debug.Log("Highscore actualizado: " + highScore);
    }

    private void UpdateHighScoreUI()
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
            OnHighScoreUpdated.Invoke();
        }
    }
}