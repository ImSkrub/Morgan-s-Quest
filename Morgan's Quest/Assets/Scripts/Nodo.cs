using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Nodo<T>
{
    public T Datos; // Datos del nodo
    public Nodo<T> Siguiente; // Referencia al siguiente nodo

    public Nodo(T datos)
    {
        Datos = datos;
        Siguiente = null;
    }
}