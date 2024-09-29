using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaTF : MonoBehaviour
{

    private GameObject[] cola;  
    private int frente;         
    private int final;          
    private int tamaño;         // Número actual de elementos en la cola
    private int capacidad;      // Capacidad máxima de la cola (tamaño del array)

  
    //Inicializar
    public ColaTF(int capacidad)
    {
        this.capacidad = capacidad;
        cola = new GameObject[capacidad];
        frente = 0;
        final = 0;
        tamaño = 0;
    }

 
    public void Enqueue(GameObject obj)
    {
        if (tamaño == capacidad)
        {
            Debug.LogWarning("La cola está llena. No se puede encolar más elementos.");
            return;
        }

       
        cola[final] = obj;

       
        final = (final + 1) % capacidad;
        tamaño++;
    }

   
    public GameObject Dequeue()
    {
        if (tamaño == 0)
        {
            Debug.LogWarning("La cola está vacía.");
            return null;
        }

        GameObject obj = cola[frente];

        frente = (frente + 1) % capacidad;
        tamaño--;

        return obj;
    }

   
    public GameObject Peek()
    {
        if (tamaño == 0)
        {
            Debug.LogWarning("La cola está vacía.");
            return null;
        }

        return cola[frente];
    }

    public int Count()
    {
        return tamaño;
    }

    public bool IsFull()
    {
        return tamaño == capacidad;
    }

    public bool IsEmpty()
    {
        return tamaño == 0;
    }
}

