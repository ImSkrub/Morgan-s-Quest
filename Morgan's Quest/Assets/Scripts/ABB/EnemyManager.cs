using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABB_EnemyPriority
{
    public class EnemyManager : MonoBehaviour
    {
        private ABB enemigos = new ABB();
        public Transform player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null)
            {
                Debug.LogError("No se encontró al jugador. Asegúrate de que el jugador tenga la etiqueta 'Player'.");
            }
        }

        // Registro de enemigo
        public void RegisterEnemy(Enemy enemy)
        {
            float distance = Vector2.Distance(enemy.transform.position, player.position);
            enemigos.AgregarElem(enemy.name, distance);
        }

        // Eliminación de enemigo por su nombre
        public void UnregisterEnemy(Enemy enemy)
        {
            enemigos.EliminarElem(enemy.name);  // Pasamos el nombre del enemigo, no la distancia
        }

        // Obtener el enemigo más cercano
        public string GetClosestEnemy()
        {
            return enemigos.EnemigoMasCercano();
        }

        // Obtener el enemigo más lejano
        public string GetFarthestEnemy()
        {
            return enemigos.EnemigoMasLejano();
        }

        private void Update()
        {
            // Aquí puedes llamar a RecalculateDistances si es necesario
            RecalculateDistances();
        }

        private void RecalculateDistances()
        {
            // Aquí puedes recalcular las distancias si es necesario, dependiendo de tu lógica.
        }
    }
}