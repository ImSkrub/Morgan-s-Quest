using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardPanel;
    public void GoToMenu()
    {
        LevelManager.instance.LoadLevel(0);
    }
    public void RestartGame()
    {
        LevelManager.instance.RestartLevel();
    }
    public void ButtonPlay()
    {
        LevelManager.instance.LoadLevel(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Config()
    {
        SceneManager.LoadScene(5);
    }
    public void LvlSelect()
    {
        SceneManager.LoadScene(12);
    }

    public void ShowLeaderboard()
    {
        if (leaderboardPanel != null)
        {
            leaderboardPanel.SetActive(true);
        }
    }
    public void CloseLeaderboard()
    {
        if (leaderboardPanel != null)
        {
            leaderboardPanel.SetActive(false);
        }
    }

    //Levels
    public void Level1()
    {
        LevelManager.instance.LoadLevel(1);
    }
    public void Level2()
    {
        LevelManager.instance.LoadLevel(2);
    }
    public void Level3()
    {
        LevelManager.instance.LoadLevel(3);
    }
    public void Level4()
    {
        LevelManager.instance.LoadLevel(4);
    }
  
}
