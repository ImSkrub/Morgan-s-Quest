using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItem : MonoBehaviour
{
    [SerializeField] private GameObject essencePrefab;

    public void SpawnItem()
    {
        if (essencePrefab != null)
        {
            Instantiate(essencePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Essence Prefab no está asignado en el inspector.");
        }
    }
}