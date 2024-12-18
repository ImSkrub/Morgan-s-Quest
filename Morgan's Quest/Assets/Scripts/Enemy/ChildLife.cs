using ABB_EnemyPriority;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLife : MonoBehaviour
{
    public float health = 25f;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float destroyDelay = 0.5f;
    [SerializeField] private GenerateItem item;
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private GameObject floatingTextPrefab;

    private ABB enemyTree;
    private Transform player;
    private float lastDistance;
    public event Action OnDeath;

    private AudioSource audioSource;
    [SerializeField] private AudioClip damageSound;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        item = GetComponent<GenerateItem>();

        enemyTree = new ABB();
        enemyTree.InicializarArbol();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            lastDistance = Vector2.Distance(transform.position, player.position);
            enemyTree.AgregarElem(gameObject.name, lastDistance);
        }
        else
        {
            Debug.LogError("Player not found. Ensure it has the 'Player' tag.");
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isDead && player != null)
        {
            float currentDistance = Vector2.Distance(transform.position, player.position);

            if (Mathf.Abs(currentDistance - lastDistance) > 0.1f)
            {
                enemyTree.EliminarElem(gameObject.name);
                enemyTree.AgregarElem(gameObject.name, currentDistance);
                lastDistance = currentDistance;
            }

            if (currentDistance < 1.5f)
            {
                AttackClosestEnemy();
            }
        }
    }

    private void AttackClosestEnemy()
    {
        string closestEnemy = enemyTree.EnemigoMasCercano();
        Debug.Log($"Attacking closest enemy: {closestEnemy}");
    }

    public void GetDamage(int value)
    {
        ShowDamage(value.ToString());

        if (isDead) return;

        health -= value;

        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        SpawnDamageParticles();

        if (health <= 0f)
        {
            Die();
        }
        else
        {
            spriteRenderer.color = damageColor;
            Invoke(nameof(RestoreColor), 0.5f);
        }
    }

    private void SpawnDamageParticles()
    {
        if (damageParticles != null)
        {
            Instantiate(damageParticles, transform.position, Quaternion.identity);
        }
    }

    private void RestoreColor()
    {
        if (!isDead)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        PointManager.Instance.AddScore(10);
        enemyTree.EliminarElem(gameObject.name);
        OnDeath?.Invoke();

        // Disminuir el contador de enemigos en el GameManager
        GameManager.Instance.DecreaseCounter();

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this) script.enabled = false;
        }

        SpawnDeathParticles();

        item?.SpawnItem();
        Destroy(gameObject);
    }

    private void SpawnDeathParticles()
    {
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
    }

    void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }

    public string GetClosestEnemy()
    {
        return enemyTree.EnemigoMasCercano();
    }

    public string GetFarthestEnemy()
    {
        return enemyTree.EnemigoMasLejano();
    }
}
