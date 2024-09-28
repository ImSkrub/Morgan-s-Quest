using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Daño
    private float damage;
    [SerializeField] private float speed = 10f;
    public float activeRadius = 20f;
    private BulletPool poolBalas;

    public void Initialize(BulletPool pool)
    {
        this.poolBalas = pool;
    }


    private void Update()
    {
        damage = Estadisticas.Instance.dano;

        if (Mathf.Abs(transform.position.x) > activeRadius || Mathf.Abs(transform.position.y) > activeRadius)
        {
            DeactivateBullet();
        }

    }



    // Colision pared con bala.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<ChildLife>().GetDamage(damage); 
            DeactivateBullet(); //Desactivar la bala.
        }
        if (collision.gameObject.layer == 11)
        {
            DeactivateBullet();
        }
        if (collision.gameObject.layer == 12)
        {
            collision.gameObject.GetComponent<ChildLife>().GetDamage(damage);
            DeactivateBullet();
        }
    }

    //Método para desactivar la bala.
    private void DeactivateBullet()
    {
        poolBalas.ReturnToPool(this.g);
    }
}
