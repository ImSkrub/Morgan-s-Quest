
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int counter = 0;

    // Player
    [SerializeField] private GameObject player;

    // Stats
    public int escence = 0;
    public TextMeshProUGUI textCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            player.GetComponent<LifePlayer>().OnDeath += LoseGame; // Subscribe to death event
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        textCount.text = "Enemy deaths: " + counter;

        // Exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (LevelManager.instance.currentLevel == 1 && counter >= 10)
        {
            LevelManager.instance.LoadNextLevel();
            Estadisticas.Instance.RestarStat();
            counter = 0;
            escence = 0;
        }

        // If it's the second level
        if (LevelManager.instance.currentLevel == 2 && counter >= 20)
        {
            WinGame();
            counter = 0;
            escence = 0;
        }
    }

    public void EnemyDied()
    {
        counter += 1; // Increment the counter for enemy deaths
        PointManager.Instance.AddScore(10); // Add 10 points for enemy death
        QuickSortHS quickSortHS = FindObjectOfType<QuickSortHS>();
        quickSortHS.AgregarPuntaje(10); // Add score to leaderboard
    }

    public void WinGame()
    {
        RegistrarPuntaje();
        SceneManager.LoadScene(3); // Load victory menu
    }

    public void LoseGame()
    {
        RegistrarPuntaje();
        SceneManager.LoadScene(4); // Load defeat menu
    }

    private void RegistrarPuntaje()
    {
        PointManager.Instance.AddScore(counter); // Add score based on defeated enemies
        PointManager.Instance.SaveScore(); // Save score in PlayerPrefs
    }
}
