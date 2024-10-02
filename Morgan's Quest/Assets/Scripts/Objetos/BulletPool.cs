using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance; // Instancia estática para el patrón Singleton
    public Bullet bulletPrefab; // Prefab de la bala
    public int poolSize = 10; // Número de balas en el pool
    private Cola<Bullet> bulletPool; // Cola para almacenar balas

    private void Awake()
    {
        Instance = this; // Inicializa la instancia estática
        bulletPool = new Cola<Bullet>(); // Crea la cola de balas
    }

    private void Start()
    {
        // Inicializa el pool de balas
        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab); // Crea una nueva bala
            bullet.gameObject.SetActive(false); // Desactiva la bala
            bulletPool.Enqueue(bullet); // Añade la bala al pool
        }
    }

    public Bullet GetBullet()
    {
        if (!bulletPool.IsEmpty()) // Comprueba si hay balas disponibles
        {
            Bullet bullet = bulletPool.Dequeue(); // Obtiene una bala del pool
            bullet.gameObject.SetActive(true); // Activa la bala
            return bullet; // Retorna la bala
        }
        return null; // Retorna null si no hay balas disponibles
    }

    public void ReturnBullet(Bullet bullet)
{
    bullet.gameObject.SetActive(false); // Desactiva la bala al devolverla
    bulletPool.Enqueue(bullet); // Añade la bala de nuevo al final del pool
}

}
