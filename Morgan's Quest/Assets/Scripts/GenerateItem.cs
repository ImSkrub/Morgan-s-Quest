using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateItem : MonoBehaviour
{
    [SerializeField] private Essence essencePrefab; // Prefab de essence
    [SerializeField] private GameObject powerUpPrefab; // Prefab de Power Up
    private const float spawnChance = 0.3f; // 30% chance for the power-up

    public void SpawnItem()
    {
        // Always instantiate the essence at the position of the enemy that has died
        Instantiate(essencePrefab, transform.position, Quaternion.identity);

        // Generate a random number between 0 and 1
        float randomValue = Random.Range(0f, 1f);

        // Check if the random value is less than or equal to the spawn chance
        if (randomValue <= spawnChance)
        {
            // Instantiate the power-up at the same position
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            Debug.Log("Power-up spawned!");
        }
        else
        {
            Debug.Log("Power-up not spawned.");
        }
    }
}
