using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItem : MonoBehaviour
{
    //Variables.
    public GameObject item; // Prefab del enemigo hijo
    public float delay = 2f; // Delay para generar los enemigos hijos
    public Transform spawnpoint; //Spawn del hijo 1

    //Generar item
    public void SpawnItem()
    {
        Instantiate(item, spawnpoint.position, transform.rotation);
    }
}
