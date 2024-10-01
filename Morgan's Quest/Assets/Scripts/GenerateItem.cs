using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GenerateItem : MonoBehaviour
{
    [SerializeField] private Essence essencePrefab; // Prefab de essence

    public void SpawnItem()
    {
        // Generar la esencia en la posición del enemigo que ha muerto
        Instantiate(essencePrefab, transform.position, Quaternion.identity);
    }
}
