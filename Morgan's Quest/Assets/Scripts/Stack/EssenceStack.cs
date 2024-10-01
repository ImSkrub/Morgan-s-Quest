using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EssenceStackTDA
{
    void InitializeStack();
    void Push(Essence essence);
    Essence Pop();
    bool IsEmpty();
    Essence Peek();
}

// Clase que representa un nodo en la pila
public class EssenceNode // Renombrado para evitar conflictos
{
    public Essence Essence;
    public EssenceNode Next;

    // Constructor
    public EssenceNode(Essence essence)
    {
        Essence = essence;
        Next = null;
    }
}

// Clase que implementa la pila dinámica de essences
public class EssenceStack : MonoBehaviour, EssenceStackTDA
{
    private EssenceNode top;

    public void InitializeStack()
    {
        top = null; // Inicializa la pila como vacía
    }

    public void Push(Essence essence)
    {
        EssenceNode newNode = new EssenceNode(essence); // Usar el constructor
        newNode.Next = top;
        top = newNode;
    }

    public Essence Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("La pila está vacía."); // Lanza una excepción si la pila está vacía
        }

        Essence essence = top.Essence; // Guarda el objeto Essence que está en la parte superior
        top = top.Next;                 // Actualiza el "top" al siguiente nodo
        return essence;                 // Devuelve el objeto Essence desapilado
    }

    public bool IsEmpty()
    {
        return top == null; // Retorna verdadero si la pila está vacía
    }

    public Essence Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("La pila está vacía."); // Lanza una excepción si la pila está vacía
        }
        return top.Essence; // Devuelve el objeto Essence que está en la parte superior sin desapilarlo
    }

    // Método opcional para visualizar el contenido de la pila
    public void PrintStack()
    {
        EssenceNode current = top; // Comienza desde el nodo superior
        while (current != null)
        {
            Debug.Log("Essence: " + current.Essence.name); // Muestra el nombre de cada objeto Essence en la pila
            current = current.Next; // Avanza al siguiente nodo
        }
    }
}