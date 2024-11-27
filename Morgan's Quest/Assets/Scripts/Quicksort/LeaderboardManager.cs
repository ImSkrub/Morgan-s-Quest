using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardPanel;
    public TextMeshProUGUI leaderboardText;
    public Button leaderboardButton;
    private QuickSortHS quickSortHS;

    private void Start()
    {
        leaderboardButton.onClick.AddListener(ShowLeaderboard);

        quickSortHS = FindObjectOfType<QuickSortHS>();
    }

    private void ShowLeaderboard()
    {
        quickSortHS.GenerarPuntajes();
        quickSortHS.OrdenarPuntajes();

        quickSortHS.MostrarPuntajes();

        leaderboardPanel.SetActive(true);
    }


    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }
}