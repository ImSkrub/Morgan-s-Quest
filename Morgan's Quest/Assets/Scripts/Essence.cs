using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    [SerializeField] public int value;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión es con el jugador
        if (collision.gameObject.CompareTag("Player") && EssenceManager.instance.escenceStack != null)
        {
            // Sumar el valor de la essence al total del jugador
            GameManager.Instance.escence += value;

            // Apilar la essence en el stack
            EssenceManager.instance.escenceStack.Push(this);

            // Destruir la essence una vez que el jugador la recoja
            Destroy(gameObject);
        }
    }
}
