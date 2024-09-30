using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            // Obtiene el componente de la pila de essences en el jugador
            EssenceStack essenceStack = other.GetComponent<EssenceStack>();
            if (essenceStack != null)
            {
                // Agrega la esencia a la pila
                Essence essence = this.GetComponent<Essence>(); // Obt�n el componente Essence
                essenceStack.Push(essence); // A�ade la esencia a la pila

                // Destruye el objeto esencia despu�s de recogerlo
                Destroy(gameObject);
            }
        }
    }
}