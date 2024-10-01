using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public Bullet bulletPrefab; // Prefab de la bala
    public int poolSize = 10; // Número de balas en el pool
    private Cola<Bullet> bulletPool;

    private void Awake()
    {
        Instance = this;
        bulletPool = new Cola<Bullet>();

    }
    private void Start() 
    {
        // Inicializar el pool de balas
        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false); // Desactivamos inicialmente
            bulletPool.Enqueue(bullet); // Añadimos al pool
        }
    }

    public Bullet GetBullet()
    {
        if (!bulletPool.IsEmpty())
        {
            Bullet bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        return null; // O puedes crear nuevas balas si es necesario
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
