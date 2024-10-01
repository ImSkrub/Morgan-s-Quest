using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    private Dictionary<string, Enemy> idEnemies;


    private void Awake()
    {
        idEnemies = new Dictionary<string, Enemy>();
        foreach(var enemy in enemies)
        {
            idEnemies.Add(enemy.name, enemy);
        }
    }
    public Enemy Create(string id)
    {
        if(!idEnemies.TryGetValue(id, out Enemy enemy))
        {
            return null;
        }
        return Instantiate(enemy);
    }
    public Enemy Create()
    {
        if (enemies.Length == 0)
        {
            Debug.LogWarning("No enemies available to create.");
            return null;
        }

        // Seleccionar un enemigo aleatorio del array
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        return Instantiate(randomEnemy);
    }
    public Enemy Create(Transform t)
    {
        Enemy spawnEnemy = enemies[Random.Range(0,enemies.Length)];
        return Instantiate(spawnEnemy,t.position,Quaternion.identity);
    }
}
