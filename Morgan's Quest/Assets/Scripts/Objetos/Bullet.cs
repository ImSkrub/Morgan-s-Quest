using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Daño
    private float damage;

    private void Update()
    {
       damage = Estadisticas.Instance.dano;
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
        gameObject.SetActive(false);
    }
}
