using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    //Quien perisgue
    [SerializeField] GameObject follow;
    public Transform player;
    [Header("Atributos")]
    [SerializeField] float distToAttack;
    [SerializeField] float vel;
    [SerializeField] float attackDelay;
    [SerializeField] float closestDist;
    [SerializeField] float lerpSpeedRotation;
    private Rigidbody2D rb;
    private Vector3 enemyDirection;
    private bool isFacingRight = true;
    public int damage;
    private float currentTime;

  

    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.velocity = enemyDirection.normalized * vel;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        
        enemyDirection = follow.transform.position - transform.position;
        transform.right = Vector3.Lerp(transform.right,enemyDirection,lerpSpeedRotation*Time.deltaTime);

        if (transform.position.x < player.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x > player.position.x && isFacingRight)
        {
            Flip();
        }
    }
    
    //Colision con jugador
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentTime >= attackDelay)
        {
            //colision player
            if (collision.gameObject.layer == 3)
            {
                collision.gameObject.GetComponent<LifePlayer>().GetDamage(damage);
               // anim.SetTrigger("Attack");
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

    public void Die()
    {
        Destroy(gameObject);
    }

    //Dibujo del radio basada en Gizmos.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distToAttack);
    }

}
