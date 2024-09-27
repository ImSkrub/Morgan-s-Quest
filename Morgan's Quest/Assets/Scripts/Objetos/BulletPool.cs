using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //Pool.
    public GameObject bulletPrefab;
    public int poolSize;

    private List<GameObject> bulletPool = new List<GameObject>();

    private void Start()
    {
        // Crear balas en el pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullet.transform.parent = transform;
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        // Buscar una bala disponible en el pool
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null;
    }
}
