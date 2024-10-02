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

    public void Fire(Vector2 direction, float speed)
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Colisi�n con pared (Layer 6)
        if (collision.gameObject.layer == 6)
        {
            BulletPool.Instance.ReturnBullet(this);
        }

        // Colisi�n con enemigo (Layer 7)
        if (collision.gameObject.layer == 7)
        {
            // Retornar la bala al pool
            BulletPool.Instance.ReturnBullet(this);

            // Obtener el componente de vida del enemigo y aplicar da�o
            ChildLife enemyLife = collision.gameObject.GetComponent<ChildLife>();
            if (enemyLife != null)
            {
                enemyLife.GetDamage(damage);

                // Si la vida del enemigo es 0 o menos, destruir el enemigo
                if (enemyLife.health <= 0) // Cambiado de currentLife a health
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    // Cuando la bala salga de la c�mara
    private void OnBecameInvisible()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
}