using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private EnemyFactory factory;
    [SerializeField] [Range(5, 20f)] private float spawnTime = 10f;
    private float nextSpawnTime;
    [SerializeField] private List<Transform> spawnpoints = new List<Transform>();
    
    // Máximo número de enemigos permitidos en pantalla
    [SerializeField] private int maxEnemies = 10;
    private int currentEnemyCount = 0;

    void Start()
    {
        nextSpawnTime = Time.time + spawnTime; // Inicializar el temporizador de spawn
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentEnemyCount < maxEnemies)
        {
            foreach (Transform t in spawnpoints)
            {
                if (t != null)
                {
                    // Crear el enemigo y suscribirse al evento OnDeath
                    Enemy enemy = factory.Create(t);
                    if (enemy != null)
                    {
                        ChildLife childLife = enemy.GetComponent<ChildLife>();
                        if (childLife != null)
                        {
                            childLife.OnDeath += OnEnemyDestroyed; // Subscribe to the event
                        }
                        currentEnemyCount++;
                    }
                }
            }

            nextSpawnTime = Time.time + spawnTime; // Establecer el próximo tiempo de spawn
        }
    }

    // Método que se llama cuando un enemigo muere
    private void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
