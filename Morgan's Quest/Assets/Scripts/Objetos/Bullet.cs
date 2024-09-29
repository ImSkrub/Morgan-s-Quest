using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = Estadisticas.Instance.dano;
    }

    public void Fire(Vector2 direction,float speed)
    {
      rb.velocity = direction*speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //colision con pared
        if (collision.gameObject.layer == 6)
        {
         BulletPool.Instance.ReturnBullet(this);
        }
        //enemy
        if(collision.gameObject.layer == 7)
        {
            BulletPool.Instance.ReturnBullet(this);
            collision.gameObject.GetComponent<ChildLife>().GetDamage(damage);
        }
    }

    //Salga de la camara
    private void OnBecameInvisible()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
    
}
