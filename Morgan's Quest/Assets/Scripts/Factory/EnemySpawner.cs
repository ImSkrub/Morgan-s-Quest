using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory factory;
    [SerializeField] [Range(1,2.5f)]private float spawnTime = 2f;
    private float nextSpawnTime;
    [SerializeField] private List<Transform> spawnpoint = new List<Transform>();

    void Start()
    {
        nextSpawnTime = Time.time + spawnTime; // Iniciar el contador en Start
        Debug.Log("Spawn comenzar� en: " + nextSpawnTime);
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

            nextSpawnTime = Time.time + spawnTime; // Establecer el pr�ximo tiempo de spawn
            Debug.Log("Pr�ximo spawn en: " + nextSpawnTime);
        }
    }

}
