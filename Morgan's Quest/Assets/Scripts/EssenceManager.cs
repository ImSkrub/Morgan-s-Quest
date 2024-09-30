using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceManager : MonoBehaviour
{
    private EssenceStack essenceStack; // Pila de essences

    [SerializeField] private Essence essencePrefab; // Prefab de essence

    private void Awake()
    {
        essenceStack = gameObject.AddComponent<EssenceStack>(); // Añadir el componente de pila
        essenceStack.InitializeStack(); // Inicializar la pila
    }

    // Método para agregar essence a la pila
    public void AddEssence()
    {
        Essence newEssence = Instantiate(essencePrefab); // Crear una nueva essence desde el prefab
        essenceStack.Push(newEssence); // Apilar la nueva essence
        Debug.Log("Essence añadida a la pila. Total en la pila: " + essenceStack.Peek().name);
    }

    // Método para recoger la esencia (desapilar)
    public void CollectEssence()
    {
        if (!essenceStack.IsEmpty())
        {
            Essence collectedEssence = essenceStack.Pop(); // Desapilar la essence
            Debug.Log("Essence recogida: " + collectedEssence.name);
            Destroy(collectedEssence.gameObject); // Destruir el objeto de essence recogido
        }
        else
        {
            Debug.Log("No hay essences en la pila.");
        }
    }
}
