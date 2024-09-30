using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    // Aquí puedes agregar propiedades y métodos para tu clase Essence.
    // Por ejemplo, un campo para almacenar un valor.
    public string essenceName;

    private void Start()
    {
        // Inicialización, si es necesaria.
        essenceName = name; // Usa el nombre del GameObject como nombre de esencia.
    }
}
