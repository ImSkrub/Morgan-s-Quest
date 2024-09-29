using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnView : MonoBehaviour
{
    [SerializeField] GameObject follow;
    public Transform player;
    [SerializeField] float distToAttack;
    [SerializeField] float Vel;
    [SerializeField] float attackDelay;
    [SerializeField] float closestDist;
    [SerializeField] float lerpSpeedRotation;
    Rigidbody2D rb;
    Vector3 enemyDirection;
    //Animator anim;
    private bool isFacingRight = true;
    public int damage;
    private float currentTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = enemyDirection.normalized *Vel;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        //Movimiento.
        enemyDirection = follow.transform.position - transform.position;
        transform.right = Vector3.Lerp(transform.right, enemyDirection, lerpSpeedRotation * Time.deltaTime);
        if (enemyDirection.magnitude > distToAttack || enemyDirection.magnitude < closestDist)
        {
            enemyDirection = Vector3.zero;
        }
        if (transform.position.x < player.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x > player.position.x && isFacingRight)
        {
            Flip();
        }
    }
    //Deteccion del radio basada en Gizmos.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distToAttack);
    }

    //Colision con jugador
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentTime >= attackDelay)
        {
            if (collision.gameObject.layer == 3)
            {
                collision.gameObject.GetComponent<LifePlayer>().GetDamage(damage);
                currentTime = 0;
            }
        }
    }

    //Giro.
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    public void OnDeath()
    {
        //anim.SetTrigger("Death");
        Destroy(gameObject);
    }
}

