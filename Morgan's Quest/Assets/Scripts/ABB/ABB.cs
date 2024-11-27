using ABB_EnemyPriority;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABB : ABBTDA
{
    NodoABB raiz;

    public string Enemigo()
    {
        return raiz.enemigo;
    }

    public float Distancia()
    {
        return raiz.distancia;
    }

    public bool ArbolVacio()
    {
        return (raiz == null);
    }

    public void InicializarArbol()
    {
        raiz = null;
    }

    public ABBTDA HijoDer()
    {
        return raiz.hijoDer;
    }

    public ABBTDA HijoIzq()
    {
        return raiz.hijoIzq;
    }

    public void AgregarElem(string enemigo, float distancia)
    {
        if (raiz == null)
        {
            raiz = new NodoABB
            {
                enemigo = enemigo,
                distancia = distancia,
                hijoIzq = new ABB(),
                hijoDer = new ABB()
            };
            raiz.hijoIzq.InicializarArbol();
            raiz.hijoDer.InicializarArbol();
        }
        else if (distancia < raiz.distancia)
        {
            raiz.hijoIzq.AgregarElem(enemigo, distancia);
        }
        else if (distancia > raiz.distancia)
        {
            raiz.hijoDer.AgregarElem(enemigo, distancia);
        }
    }

    public void EliminarElem(string enemigo)
    {
        if (raiz != null)
        {
            if (raiz.enemigo == enemigo && raiz.hijoIzq.ArbolVacio() && raiz.hijoDer.ArbolVacio())
            {
                raiz = null;
            }
            else if (raiz.enemigo == enemigo && !raiz.hijoIzq.ArbolVacio())
            {
                raiz.distancia = Mayor(raiz.hijoIzq);
                raiz.enemigo = EnemigoMayor(raiz.hijoIzq);
                raiz.hijoIzq.EliminarElem(raiz.enemigo);
            }
            else if (raiz.enemigo == enemigo && raiz.hijoIzq.ArbolVacio())
            {
                raiz.distancia = Menor(raiz.hijoDer);
                raiz.enemigo = EnemigoMenor(raiz.hijoDer);
                raiz.hijoDer.EliminarElem(raiz.enemigo);
            }
            else if (string.Compare(enemigo, raiz.enemigo) > 0)
            {
                raiz.hijoDer.EliminarElem(enemigo);
            }
            else
            {
                raiz.hijoIzq.EliminarElem(enemigo);
            }
        }
    }

    public string EnemigoMasCercano()
    {
        if (raiz.hijoIzq.ArbolVacio())
        {
            return raiz.enemigo;
        }
        else
        {
            return ((ABB)raiz.hijoIzq).EnemigoMasCercano();
        }
    }

    public string EnemigoMasLejano()
    {
        if (raiz.hijoDer.ArbolVacio())
        {
            return raiz.enemigo;
        }
        else
        {
            return ((ABB)raiz.hijoDer).EnemigoMasLejano();
        }
    }

    private float Mayor(ABBTDA arbol)
    {
        if (arbol.HijoDer().ArbolVacio())
        {
            return arbol.Distancia();
        }
        else
        {
            return Mayor(arbol.HijoDer());
        }
    }

    private float Menor(ABBTDA arbol)
    {
        if (arbol.HijoIzq().ArbolVacio())
        {
            return arbol.Distancia();
        }
        else
        {
            return Menor(arbol.HijoIzq());
        }
    }

    private string EnemigoMayor(ABBTDA arbol)
    {
        if (arbol.HijoDer().ArbolVacio())
        {
            return arbol.Enemigo();
        }
        else
        {
            return EnemigoMayor(arbol.HijoDer());
        }
    }

    private string EnemigoMenor(ABBTDA arbol)
    {
        if (arbol.HijoIzq().ArbolVacio())
        {
            return arbol.Enemigo();
        }
        else
        {
            return EnemigoMenor(arbol.HijoIzq());
        }
    }

    // Método de recorrido inorden
    public void RecorridoInorden()
    {
        RecorridoInordenRecursivo(raiz);
    }

    private void RecorridoInordenRecursivo(NodoABB nodo)
    {
        if (nodo != null)
        {

            RecorridoInordenRecursivo((NodoABB)nodo.hijoIzq); // Cambiado aquí

            // Visitar nodo actual
            Console.WriteLine("Enemigo: " + nodo.enemigo + ", Distancia: " + nodo.distancia);


            RecorridoInordenRecursivo((NodoABB)nodo.hijoDer); // Cambiado aquí
        }
    }


    private void RecorridoPreordenRecursivo(NodoABB nodo)
    {
        if (nodo != null)
        {
            // Visitar nodo actual
            Console.WriteLine("Enemigo: " + nodo.enemigo + ", Distancia: " + nodo.distancia);

            // Recorrer subárbol izquierdo
            RecorridoPreordenRecursivo((NodoABB)nodo.hijoIzq);

            // Recorrer subárbol derecho
            RecorridoPreordenRecursivo((NodoABB)nodo.hijoDer);
        }
    }


    private void RecorridoPostordenRecursivo(NodoABB nodo)
    {
        if (nodo != null)
        {
            // Recorrer subárbol izquierdo
            RecorridoPostordenRecursivo((NodoABB)nodo.hijoIzq);

            // Recorrer subárbol derecho
            RecorridoPostordenRecursivo((NodoABB)nodo.hijoDer);

            // Visitar nodo actual
            Console.WriteLine("Enemigo: " + nodo.enemigo + ", Distancia: " + nodo.distancia);
        }
    }
}