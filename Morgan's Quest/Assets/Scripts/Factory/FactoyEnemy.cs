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
            int damage = ObtenerDañoPorTipo(enemyPrefab);
            enemy.Add(enemyPrefab, damage);
        }
    }

    public void GenerarEnemigo(GameObject tipoEnemigo)
    {
        //¿Enemigo existe en el diccionario? 
        if (enemy.ContainsKey(tipoEnemigo))
        {
            int daño = enemy[tipoEnemigo]; //Si, sacar su daño

            //Generar uno
            GameObject nuevoEnemigo = Instantiate(tipoEnemigo, transform.position, Quaternion.identity);

            //Generar daño
            nuevoEnemigo.GetComponent<AttackOnView>().damage = daño;
        }
        else
        {
            Debug.LogError("NO enemigo."); //No.
        }
    }

    private int ObtenerDañoPorTipo(GameObject tipoEnemigo)
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
