using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    // Aqu� puedes agregar propiedades y m�todos para tu clase Essence.
    // Por ejemplo, un campo para almacenar un valor.
    public string essenceName;

    private void Start()
    {
        // Inicializaci�n, si es necesaria.
        essenceName = name; // Usa el nombre del GameObject como nombre de esencia.
    }
}
