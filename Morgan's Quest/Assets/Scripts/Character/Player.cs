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
    private Animator anim;

    [Header("MOVIMIENTO")]
    //Velocidad
    private int speed;
    [SerializeField][Range(1,10)] private float acceleration = 3f;
    [SerializeField][Range(1, 10)] private float deceleration = 10f;
    [SerializeField] private float maxSpeed = 10f;
    private Vector2 currentVelocity = Vector2.zero;
    [SerializeField] private float moveForce = 50f;

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

    [Header("STATS")]
    //Estadisticas del personaje.
    public TextMeshProUGUI Velocidad;
    public TextMeshProUGUI Daño;
    public TextMeshProUGUI Escence;

    public bool isShooting=false;

    //Mana
    private ManaPlayer Mana;

    private void Start()
    {
        essenceStack = gameObject.AddComponent<EssenceStack>(); // Añade el componente de la pila
        essenceStack.InitializeStack(); // Inicializa la pila
        bulletPool = new BulletPool();
        rigidbody = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        Mana= GetComponent<ManaPlayer>();
        originalBulletSpeed = bulletSpeed;
    }

    private void Update()
    {
        speed = Estadisticas.Instance.vel;
        //Textos del canvas que marcan estadisticas.
        Velocidad.text = "Velocidad " + speed;
        Daño.text = "Daño " + Estadisticas.Instance.dano;
        Escence.text = "Escencias " + GameManager.Instance.escence;

        if (Input.GetKey(KeyCode.G))
        {
            bulletSpeed = 1f; 
        }
        else
        {
            bulletSpeed = originalBulletSpeed;
        }

        //Ataques del personaje (disparo)
        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");
       

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
    
        //rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);

    }
    private void FixedUpdate()
    {
        //Movimiento del personaje
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");
        Vector2 inputDirection = new Vector2(horizontal, vertical).normalized;
       
        if (inputDirection.magnitude > 0)
        {
            currentVelocity += inputDirection * acceleration * Time.deltaTime;
            currentVelocity = Vector2.ClampMagnitude(currentVelocity, maxSpeed);  // Limitar a la velocidad máxima
           
        }
        else
        {
            // Desaceleración cuando no hay input
            
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * Time.deltaTime);
        }

        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(inputDirection * moveForce);
        }

        rigidbody.velocity = currentVelocity;
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
