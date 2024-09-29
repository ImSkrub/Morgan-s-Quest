using System.Collections.Generic;

public class Cola<T>
{
    private List<T> elementos;

    public Cola()
    {
        elementos = new List<T>();
    }

    // Agrega un elemento al final de la cola
    public void Enqueue(T elemento)
    {
        elementos.Add(elemento);
    }

    // Elimina y retorna el elemento al frente de la cola
    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new System.InvalidOperationException("La cola está vacía.");
        }
        T elemento = elementos[0];
        elementos.RemoveAt(0);
        return elemento;
    }

    // Retorna el elemento al frente sin eliminarlo
    public T Peek()
    {
        if (IsEmpty())
        {
            throw new System.InvalidOperationException("La cola está vacía.");
        }
        return elementos[0];
    }

    // Verifica si la cola está vacía
    public bool IsEmpty()
    {
        return elementos.Count == 0;
    }

    // Retorna la cantidad de elementos en la cola
    public int Size()
    {
        return elementos.Count;
    }
}
