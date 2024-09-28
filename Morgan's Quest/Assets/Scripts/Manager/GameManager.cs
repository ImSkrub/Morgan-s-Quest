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
    [SerializeField] GameObject player;

    //Stats and
    //
    public float escence;
    public TextMeshProUGUI textCount;
    public GameObject stats;
    private bool activeStats = true;


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
        DontDestroyOnLoad(gameObject);
        player.GetComponent<LifePlayer>().OnDeath += FinishGame;
    }

    private void Update()
    {
        //Exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        
    }

    public void CompleteGame()
    {
      
        SceneManager.LoadScene(4);
  
    }

    public void FinishGame()
    {
       
        SceneManager.LoadScene(5);

    }

   
}
