//using ABB_EnemyPriority;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int counter = 10; // Inicia el contador en el valor máximo para el primer nivel.
    public int escence = 0;

    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI textCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        player.GetComponent<LifePlayer>().OnDeath += LoseGame;
    }

    private void Update()
    {
        textCount.text = "Enemies remaining: " + counter;

        // Salir del juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        // Cambiar de nivel cuando el contador llega a 0
        if (counter <= 0)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        if (LevelManager.instance.currentLevel == 1)
        {
            LevelManager.instance.LoadNextLevel();
            Estadisticas.Instance.RestarStat();
            counter = 20; // Reinicia el contador para el siguiente nivel.
            escence = 0;
        }
        else if (LevelManager.instance.currentLevel == 2)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        SceneManager.LoadScene(3); // Escena de victoria
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(4); // Escena de derrota
    }

    // Método para reducir el contador
    public void DecreaseCounter()
    {
        if (counter > 0)
        {
            counter--;
        }
    }
}