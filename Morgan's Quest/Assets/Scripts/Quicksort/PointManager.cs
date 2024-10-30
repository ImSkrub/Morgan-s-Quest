using UnityEngine;
using System.Collections.Generic;

public class PointManager : MonoBehaviour
{
    [SerializeField] public Estadisticas estadisticas;

    //Variable para mantener un registro de las puntuaciones anteriores.
    private Stack<int> scoreHistory = new Stack<int>();
     

    private static PointManager instance;

    public static PointManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (PointManager.Instance == null)
        {
            PointManager.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Stack<int> GetScoreHistory()
    {
        // no saca 
        return scoreHistory;
    }

    public void SaveFinalScore()
    {
        scoreHistory.Push(estadisticas.puntos);
    }
}
 