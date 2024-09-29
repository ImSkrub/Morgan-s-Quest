using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaTF : MonoBehaviour
{

    private GameObject[] cola;  
    private int frente;         
    private int final;          
    private int tama�o;         // N�mero actual de elementos en la cola
    private int capacidad;      // Capacidad m�xima de la cola (tama�o del array)

  
    //Inicializar
    public ColaTF(int capacidad)
    {
        this.capacidad = capacidad;
        cola = new GameObject[capacidad];
        frente = 0;
        final = 0;
        tama�o = 0;
    }

 
    public void Enqueue(GameObject obj)
    {
        if (tama�o == capacidad)
        {
            Debug.LogWarning("La cola est� llena. No se puede encolar m�s elementos.");
            return;
        }

       
        cola[final] = obj;

       
        final = (final + 1) % capacidad;
        tama�o++;
    }

   
    public GameObject Dequeue()
    {
        if (tama�o == 0)
        {
            Debug.LogWarning("La cola est� vac�a.");
            return null;
        }

        GameObject obj = cola[frente];

        frente = (frente + 1) % capacidad;
        tama�o--;

        return obj;
    }

   
    public GameObject Peek()
    {
        if (tama�o == 0)
        {
            Debug.LogWarning("La cola est� vac�a.");
            return null;
        }

        return cola[frente];
    }

    public int Count()
    {
        return tama�o;
    }

    public bool IsFull()
    {
        return tama�o == capacidad;
    }

    public bool IsEmpty()
    {
        return tama�o == 0;
    }
}

