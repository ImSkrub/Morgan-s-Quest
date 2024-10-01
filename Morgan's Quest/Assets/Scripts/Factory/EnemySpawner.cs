using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory factory;
    [SerializeField] [Range(1,2.5f)]private float spawnTime = 2f;
    private float nextSpawnTime;
    [SerializeField] private List<Transform> spawnpoint = new List<Transform>();

    void Update()
    {
        if (Time.deltaTime >= nextSpawnTime)
        {
            foreach (Transform t in spawnpoint)
            {
              factory.Create(t); 
            }
            nextSpawnTime = Time.deltaTime + spawnTime; // Establecer el próximo tiempo de spawn
        }
    }

}
