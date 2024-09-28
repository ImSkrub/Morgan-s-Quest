using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IShoot, IMovable
{
    //Variables de interfaz IShoot//
    public BulletPool BulletPool=>bulletPool;
    public float BulletSpeed=>bulletSpeed;
    public float FireRate => fireRate;
    public float NextFire => nextFire;

    //Variables de interfaz IMovable//
    public int Speed => speed;
    
    private Rigidbody2D rigidbody;
    public Rigidbody2D Rigidbody => rigidbody;

    private float currentTime;

    //Animaciones.
    private Animator anim;

    //Velocidad
    private int speed;

    //Bala, objeto y disparo//
    [SerializeField] private BulletPool bulletPool;
    private float bulletSpeed = 50f;
    private float fireRate = 0.2f;
    private float nextFire = 0f;

    //Estadisticas del personaje.
    public TextMeshProUGUI Velocidad;
    public TextMeshProUGUI Daño;
    public TextMeshProUGUI Escence;

    public bool isShooting=false;

    //Mana
    private ManaPlayer Mana;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        Mana= GetComponent<ManaPlayer>();
    }

    private void Update()
    {
        speed = Estadisticas.Instance.vel;
        //Textos del canvas que marcan estadisticas.
        Velocidad.text = "Velocidad " + speed;
        Daño.text = "Daño " + Estadisticas.Instance.dano;
        Escence.text = "Escencias " + GameManager.Instance.escence;

        //Movimiento del personaje
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

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
           ////AudioManager.instance.PlaySound(0);
           //GameObject bullet = bulletPool.GetBullet(); //Bala del pool.
           //bullet.transform.position = transform.position;
           //bullet.SetActive(true);
           //Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
           //Vector2 shootDirection = new Vector2(x, y).normalized; //Dirección
           //bulletRigidbody.velocity = shootDirection * bulletSpeed;

           //Mana.
           Mana.currentMana -= 20;
        }
    }
}
