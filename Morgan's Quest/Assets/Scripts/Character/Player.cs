using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IShoot, IMovable
{
    // Interface variables for shooting
    public BulletPool BulletPool => bulletPool;

    private EssenceStack essenceStack;

    public float BulletSpeed => bulletSpeed;
    public float FireRate => fireRate;
    public float NextFire => nextFire;

    // Interface variables for movement
    public int Speed => speed;

    private Rigidbody2D rb;
    public Rigidbody2D Rigidbody => rb;

    // Animation variables
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public enum PlayerStates
    {
        IDLE,
        WALK
    }

    private PlayerStates currentState;
    private bool stateLock = false;

    [Header("Movement Settings")]
    [SerializeField] private int speed = 7;
    [SerializeField] private float moveForce = 50f;

    [Header("Bullet Settings")]
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpeed = 20f;
    private float originalBulletSpeed;
    private float fireRate = 0.2f;
    private float nextFire = 0.3f;

    [Header("Stats")]
    public TextMeshProUGUI scoreText;

    private bool isShooting = false;

    // Mana
    private ManaPlayer mana;

    // Variables de disparo
    [SerializeField] private AudioClip fireballSound,noManaSound;
    private AudioSource audioSource;
    private void Start()
    {
        InitializeComponents();
        InitializeEssenceStack();
        originalBulletSpeed = bulletSpeed;
        SetCurrentState(PlayerStates.IDLE);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    private void Update()
    {
        UpdateScoreText();
        HandleMovement();
        HandleShooting();
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mana = GetComponent<ManaPlayer>();
    }

    private void InitializeEssenceStack()
    {
        essenceStack = gameObject.AddComponent<EssenceStack>();
        essenceStack.InitializeStack();
        bulletPool = new BulletPool();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Escencias " + GameManager.Instance.escence;
    }
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movementInput = new Vector2(horizontal, vertical).normalized;

        if (movementInput != Vector2.zero)
        {
            SetCurrentState(PlayerStates.WALK);
            UpdateAnimatorParameters(movementInput);
        }
        else
        {
            SetCurrentState(PlayerStates.IDLE);
        }

        // Update animator parameters for movement
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", movementInput.magnitude);

        rb.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
    }

    private void UpdateAnimatorParameters(Vector2 movementInput)
    {
        animator.SetFloat("xMove", movementInput.x);
        animator.SetFloat("yMove", movementInput.y);
    }

    private void HandleShooting()
    {
        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > nextFire)
        {
            // Lógica de disparo
            Shoot(shootHorizontal, shootVertical);
            nextFire = Time.time + fireRate;
        }
    }

    public void Shoot(float x, float y)
    {
        if (mana.currentMana < 20)
        {
            audioSource.PlayOneShot(noManaSound,0.3f);
            return;
        }
        else
        {
            audioSource.PlayOneShot(fireballSound, 0.4f);
        }

        isShooting = true;
        Bullet bullet = BulletPool.Instance.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = transform.position; // Set bullet position
            bullet.Fire(new Vector2(x, y), bulletSpeed); // Fire bullet
            mana.currentMana -= 20; // Deduct mana cost
        }
    }

    private void SetCurrentState(PlayerStates newState)
    {
        if (!stateLock)
        {
            currentState = newState;

            switch (currentState)
            {
                case PlayerStates.IDLE:
                    animator.Play("Idle");
                    break;
                case PlayerStates.WALK:
                    animator.Play("Walk");
                    break;
            }
        }
    }
}
