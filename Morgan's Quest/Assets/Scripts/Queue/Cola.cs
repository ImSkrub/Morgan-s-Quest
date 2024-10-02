using System;

public class Cola<T>
{
    private Nodo<T> primero; // Primer nodo de la cola
    private Nodo<T> ultimo; // �ltimo nodo de la cola
    private int count; // Cantidad de elementos en la cola

    public Cola()
    {
        primero = null;
        ultimo = null;
        count = 0; // Inicializa el contador
    }

    // Agrega un elemento al inicio de la cola
    public void Enqueue(T elemento)
    {
        Nodo<T> nuevo = new Nodo<T>(elemento); // Crea un nuevo nodo

        if (count == 0)
        {
            primero = nuevo; // Si est� vac�o, el nuevo es el primero y �ltimo
            ultimo = nuevo;
        }
        else
        {
            nuevo.Siguiente = primero; // El nuevo nodo apunta al primer nodo actual
            primero = nuevo; // El nuevo nodo se convierte en el primero
        }

        count++; // Incrementa el contador
    }

    // Elimina y retorna el elemento al frente de la cola
    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("La cola est� vac�a.");
        }

        T elemento = primero.Datos; // Guarda el dato del primer nodo
        primero = primero.Siguiente; // Avanza el puntero al primer nodo
        count--; // Decrementa la cantidad de elementos

        if (primero == null)
        {
            ultimo = null; // Si la cola queda vac�a, actualiza el �ltimo
        }

        return elemento; // Retorna el dato del primer nodo
    }

    // Retorna el elemento al frente sin eliminarlo
    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("La cola est� vac�a.");
        }
        return primero.Datos; // Retorna el dato del primer nodo
    }

    // Verifica si la cola est� vac�a
    public bool IsEmpty()
    {
        return count == 0; // Retorna verdadero si no hay elementos
    }

    // Retorna la cantidad de elementos en la cola
    public int Size()
    {
        return count; // Retorna el conteo total
    }
}