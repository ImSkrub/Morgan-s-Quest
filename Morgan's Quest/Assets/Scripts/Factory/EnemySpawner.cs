using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory factory;
    [SerializeField] [Range(5,20f)]private float spawnTime = 10f;
    private float nextSpawnTime;
    [SerializeField] private List<Transform> spawnpoint = new List<Transform>();

    void Start()
    {
        nextSpawnTime = Time.time + spawnTime; // Iniciar el contador en Start
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
           
            foreach (Transform t in spawnpoint)
            {
                if (t != null)
                {
                
                    factory.Create(t);
                }
               
            }

            nextSpawnTime = Time.time + spawnTime; // Establecer el próximo tiempo de spawn
          
        }
    }

}
