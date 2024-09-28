using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    public void Fire(Vector3 direction)
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.velocity = direction * speed;
    }

    private void OnBecameInvisible()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
}
