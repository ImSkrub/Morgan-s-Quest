using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABB_EnemyPriority
{
    class Program
    {
        static void Main(string[] args)
        {
            // Programa para priorizar enemigos por distancia
            Console.WriteLine("Programa Iniciado\n");

            // Creo un ABB para los enemigos
            ABB enemigos = new ABB();

            // Distancias simuladas de enemigos al jugador
            (string, float)[] enemigosInfo = {
                ("Enemigo1", 10.5f),
                ("Enemigo2", 5.0f),
                ("Enemigo3", 15.3f),
                ("Enemigo4", 2.7f),
                ("Enemigo5", 8.9f)
            };

            // Agrego los enemigos al ABB
            foreach (var enemigo in enemigosInfo)
            {
                enemigos.AgregarElem(enemigo.Item1, enemigo.Item2);
            }

            Console.WriteLine("\nEnemigo más cercano: " + enemigos.EnemigoMasCercano());
            Console.WriteLine("\nEnemigo más lejano: " + enemigos.EnemigoMasLejano());

            // Eliminamos un enemigo por nombre
            enemigos.EliminarElem("Enemigo3");

            Console.WriteLine("\nEnemigo más cercano después de eliminar: " + enemigos.EnemigoMasCercano());
            Console.WriteLine("\nEnemigo más lejano después de eliminar: " + enemigos.EnemigoMasLejano());

            Console.ReadKey();
        }
    }

    public interface ABBTDA
    {
        string Enemigo();
        float Distancia();
        ABBTDA HijoIzq();
        ABBTDA HijoDer();
        bool ArbolVacio();
        void InicializarArbol();
        void AgregarElem(string enemigo, float distancia);
        void EliminarElem(string enemigo);
    }

    public class NodoABB
    {
        public string enemigo;      // Nombre del enemigo
        public float distancia;     // Distancia al jugador
        public ABBTDA hijoIzq;
        public ABBTDA hijoDer;
    }

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
                // Verificamos si el enemigo a eliminar es el actual
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
                else if (string.Compare(enemigo, raiz.enemigo) > 0) // Si el enemigo está en el lado derecho
                {
                    raiz.hijoDer.EliminarElem(enemigo);
                }
                else // Si el enemigo está en el lado izquierdo
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
    }
}
