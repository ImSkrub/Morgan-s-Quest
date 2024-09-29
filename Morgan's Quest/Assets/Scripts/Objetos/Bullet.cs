using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 direction)
    {
      rb.AddForce(direction*speed,ForceMode2D.Impulse);
    }

    //Salga de la camara
    private void OnBecameInvisible()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
    
}
