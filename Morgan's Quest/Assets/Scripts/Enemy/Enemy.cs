using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] protected float distToAttack;
    [SerializeField] protected float vel;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected int damage;

    protected Transform player;
    protected Rigidbody2D rb;
    protected float currentTime;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("No se encontró el jugador. Asegúrate de que el jugador tenga la etiqueta 'Player'.");
        }
    }

    protected virtual void Update()
    {
        currentTime += Time.deltaTime;
    }

    protected virtual void Attack(Collision2D collision)
    {
        if (currentTime >= attackDelay && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<LifePlayer>().GetDamage(damage);
            currentTime = 0;
        }
    }

    protected virtual void Move(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + direction * vel * Time.deltaTime);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Attack(collision);
    }
}
