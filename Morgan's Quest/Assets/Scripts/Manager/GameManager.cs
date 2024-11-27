
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

    //player
    [SerializeField]private GameObject player;
    
    //Stats
    public int escence = 0;
    public TextMeshProUGUI textCount;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            player.GetComponent<LifePlayer>().OnDeath += LoseGame; // Suscribirse al evento de muerte
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
        //Exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (LevelManager.instance.currentLevel== 1 && counter >= 10)
        {
            //Pasar siguiente nivel y reiniciar las estadisticas --> puntaje
            LevelManager.instance.LoadNextLevel();
            Estadisticas.Instance.RestarStat();
            counter = 0;
            escence = 0;
        }
        //Si es el segundo nivel
        if (LevelManager.instance.currentLevel == 2 && counter >= 20)
        {
            WinGame();
            counter = 0;
            escence = 0;
        }


    }

    public void WinGame()
    {
        RegistrarPuntaje();
        SceneManager.LoadScene(3); // Cargar men� de victoria
    }

    public void LoseGame()
    {
        RegistrarPuntaje();
        SceneManager.LoadScene(4); // Cargar men� de derrota
    }

    private void RegistrarPuntaje()
    {
        // Registra el puntaje en PointManager
        PointManager.Instance.AddScore(counter); // Agrega el puntaje basado en enemigos derrotados
        PointManager.Instance.SaveScore(); // Guarda el puntaje en PlayerPrefs
    }
}
