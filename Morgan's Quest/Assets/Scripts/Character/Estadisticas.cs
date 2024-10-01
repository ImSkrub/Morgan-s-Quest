using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estadisticas : MonoBehaviour
{
    [SerializeField] public int vida=100;
    [SerializeField] public int mana=100;
    [SerializeField] public int dano=10;
    [SerializeField] public int vel=10;

    [SerializeField] public int puntos = 0;

    public static Estadisticas Instance;
     
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
            
    }

    public void RestarStat()
    {
        vida = 100;
        mana = 100;
        dano = 10;
        vel = 10;
        puntos = 0;
    }

}
