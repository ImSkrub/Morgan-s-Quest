using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IShoot, IMovable
{
    //Variables de interfaz IShoot//
    public BulletPool BulletPool => bulletPool;

    private EssenceStack essenceStack;

    public float BulletSpeed => bulletSpeed;
    public float FireRate => fireRate;
    public float NextFire => nextFire;

    //Variables de interfaz IMovable//
    public int Speed => speed;

    private Rigidbody2D rigidbody;
    public Rigidbody2D Rigidbody => rigidbody;

    private float currentTime;

    //Animaciones.
<<<<<<< HEAD
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;

    public enum playerStates
    {
        IDLE,

        WALK
    }

    bool m_stateLock = false;

    playerStates CurrentState
    {
        set {
            if (m_stateLock == false)
            {
                m_currentState = value;

                switch (m_currentState)
                {
                    case playerStates.IDLE:
                        m_animator.Play("Idle");
                        break;
                    case playerStates.WALK:
                        m_animator.Play("Walk");
                        break;
                }
            }          
        }
    }
=======
    public Animator anim;
>>>>>>> 84173f2d8f46dd9e768282a944e662485aca52af

    [Header("MOVIMIENTO")]
    //Velocidad
    [SerializeField] private int speed=7;
    private Vector2 currentVelocity = Vector2.zero;
    [SerializeField] private float moveForce = 50f;
    [Space(2)]
    //Bala, objeto y disparo//
    [Header("BULLET Y POOL")]
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpeed = 20f;
    private float originalBulletSpeed;
    private float fireRate = 0.2f;
    private float nextFire = 0.3f;
    [Space(2)]
    [Header("STATS")]
    //Estadisticas del personaje.
    
    public TextMeshProUGUI Puntaje;

    public bool isShooting=false;

    //Mana
    private ManaPlayer Mana;

    playerStates m_currentState;

    private void Start()
    {
        essenceStack = gameObject.AddComponent<EssenceStack>(); // Añade el componente de la pila
        essenceStack.InitializeStack(); // Inicializa la pila
        bulletPool = new BulletPool();
        rigidbody = GetComponent<Rigidbody2D>();
       // anim=GetComponent<Animator>();
        Mana= GetComponent<ManaPlayer>();
        originalBulletSpeed = bulletSpeed;
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Puntaje.text = "Escencias " + GameManager.Instance.escence;
        //Cambiar vel bala
        if (Input.GetKey(KeyCode.G))
        {
            bulletSpeed = 1f; 
        }
        else
        {
            bulletSpeed = originalBulletSpeed;
        }

        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        
        //Ataques del personaje (disparo)
        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");
<<<<<<< HEAD

        Vector2 movementInput = new Vector2(horizontal, vertical).normalized;

        if (movementInput != Vector2.zero)
        {
            CurrentState = playerStates.WALK;

            // Actualiza los parámetros del Animator
            m_animator.SetFloat("xMove", movementInput.x);
            m_animator.SetFloat("yMove", movementInput.y);

            // Volteo del sprite en función del eje X
            if (movementInput.x > 0)
            {
                m_spriteRenderer.flipX = false;
            }
            else if (movementInput.x < 0)
            {
                m_spriteRenderer.flipX = true;
            }
        }
        else
        {
            CurrentState = playerStates.IDLE;
        }
=======
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Speed", speed);
>>>>>>> 84173f2d8f46dd9e768282a944e662485aca52af

        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > nextFire + fireRate)
        {
            //AudioManager.instance.PlaySound(0);
            Shoot(shootHorizontal, shootVertical);
            nextFire = Time.time + fireRate;
            //<
            if (shootHorizontal < 0 && isShooting == true)
            {
               // anim.SetTrigger("ShootL");
                isShooting = false;
            }
            if (shootHorizontal > 0 && isShooting == true)
            {
               // anim.SetTrigger("ShootR");
                isShooting = false;
            }
            if (shootVertical > 0 && isShooting == true)
            {
               // anim.SetTrigger("ShootUp");
                isShooting = false;
            }
            if (shootVertical < 0 && isShooting == true)
            {
               // anim.SetTrigger("ShootD");
                isShooting = false;
            }
        }
    
       rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);

    }

    //Disparo del personaje
    public void Shoot(float x, float y)
    {
       if(Mana.currentMana <=19)
        {
          //AudioManager.instance.PlaySound(5);
        }

        isShooting = true;
        if (Mana.currentMana >= 20)
        {
            Bullet bullet = BulletPool.Instance.GetBullet();
            if (bullet != null)
            {
                bullet.transform.position = transform.position; // Ajustar la posición de la bala
                bullet.Fire(new Vector2(x,y),bulletSpeed); // Disparar en la dirección del frente
            }

            //Mana.
            Mana.currentMana -= 20;
        }
    }
}
