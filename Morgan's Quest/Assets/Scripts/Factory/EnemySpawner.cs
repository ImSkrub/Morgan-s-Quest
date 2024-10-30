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
        Debug.Log("Spawn comenzará en: " + nextSpawnTime);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Debug.Log("Spawneando enemigos en: " + Time.time);

            foreach (Transform t in spawnpoint)
            {
                if (t != null)
                {
                    Debug.Log("Spawneando en punto: " + t.position);
                    factory.Create(t);
                }
                else
                {
                    Debug.LogWarning("Punto de spawn es nulo");
                }
            }

            nextSpawnTime = Time.time + spawnTime; // Establecer el próximo tiempo de spawn
            Debug.Log("Próximo spawn en: " + nextSpawnTime);
        }
    }

}
