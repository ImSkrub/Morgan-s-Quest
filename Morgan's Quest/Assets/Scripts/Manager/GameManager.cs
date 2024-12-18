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
    public int counter = 25; // Inicia el contador en el valor máximo para el primer nivel.
    public int escence = 0;

    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI textCount;

    private bool gameEnded = false;

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
        gameEnded = false ;
    }

    private void Update()
    {
        textCount.text = "Enemies remaining: " + counter;

        // Salir del juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
           
        }

        // Verifica si se debe avanzar al siguiente nivel
        if (counter <= 0 && player.activeSelf)
        {
            NextLevel();
        }
    }

    public void WinGame()
    {
        SceneManager.LoadScene(3); // Escena de victoria
       
    }

    public void LoseGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        SceneManager.LoadScene(4); // Escena de derrota
 
    }

    private void NextLevel()
    {
        if (gameEnded) return;

        if (LevelManager.instance.currentLevel == 1)
        {
            LevelManager.instance.LoadNextLevel();
            Estadisticas.Instance.RestarStat();
            counter = 35; // Reinicia el contador para el siguiente nivel.
            escence = 0;
        }
        else if (LevelManager.instance.currentLevel == 2)
        {
            WinGame();
        }
    }


    // Método para reducir el contador
    public void DecreaseCounter()
    {
        if (counter > 0)
        {
            counter--;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 4) // Si se ha cargado la escena de derrota
        {
            gameEnded = false;
        }
    }
}