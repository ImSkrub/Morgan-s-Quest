using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace ABB_EnemyPriority
//{
//    public class EnemyManager : MonoBehaviour
//    {
//        private ABB enemigos = new ABB();
//        public Transform player;

//        private void Awake()
//        {
//            player = GameObject.FindGameObjectWithTag("Player")?.transform;
//            if (player == null)
//            {
//                Debug.LogError("No se encontró al jugador. Asegúrate de que el jugador tenga la etiqueta 'Player'.");
//            }
//        }


//        public void RegisterEnemy(Enemy enemy)
//        {
//            float distance = Vector2.Distance(enemy.transform.position, player.position);
//            enemigos.AgregarElem(enemy.name, distance);
//        }


//        public void UnregisterEnemy(Enemy enemy)
//        {
//            enemigos.EliminarElem(enemy.name);
//        }


//        public string GetClosestEnemy()
//        {
//            return enemigos.EnemigoMasCercano();
//        }


//        public string GetFarthestEnemy()
//        {
//            return enemigos.EnemigoMasLejano();
//        }

//        private void Update()
//        {

//            RecalculateDistances();
//        }

//        private void RecalculateDistances()
//        {

//        }
//    }
//}