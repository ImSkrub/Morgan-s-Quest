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
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        player.GetComponent<LifePlayer>().OnDeath += LoseGame;
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
      
        SceneManager.LoadScene(3);
  
    }

    public void LoseGame()
    {
      SceneManager.LoadScene(4);

    }

   
}
