using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoyEnemy : MonoBehaviour
{
    //Diccionario.
    public Dictionary<GameObject, int> enemy;

    public GameObject[] enemyPrefabs; //Array tipos de enemigos.

    void Start()
    {
        enemy = new Dictionary<GameObject, int>();

        foreach (GameObject enemyPrefab in enemyPrefabs)
        {
            int damage = ObtenerDa�oPorTipo(enemyPrefab);
            enemy.Add(enemyPrefab, damage);
        }
    }

    public void GenerarEnemigo(GameObject tipoEnemigo)
    {
        //�Enemigo existe en el diccionario? 
        if (enemy.ContainsKey(tipoEnemigo))
        {
            int da�o = enemy[tipoEnemigo]; //Si, sacar su da�o

            //Generar uno
            GameObject nuevoEnemigo = Instantiate(tipoEnemigo, transform.position, Quaternion.identity);

            //Generar da�o
            nuevoEnemigo.GetComponent<AttackOnView>().damage = da�o;
        }
        else
        {
            Debug.LogError("NO enemigo."); //No.
        }
    }

    private int ObtenerDa�oPorTipo(GameObject tipoEnemigo)
    {
        switch (tipoEnemigo.name)
        {
            case "EnemigoTipo1":
                return 15;
            case "EnemigoTipo2":
                return 20;
            default:
                return 0;
        }
    }

}
