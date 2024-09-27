using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LifePlayer : MonoBehaviour, IDamageable
{
    //Variables interfaz IDamageable//
    public float CurrentHealth => currentHealth;

    //Variables de vida.
    public int maxHealth; //Maximo de vida
    public float currentHealth; //Vida que llevas en el juego
    public float damageCooldown = 1f; //Da�o

    //Llamar para Pop de Pila.
  //  private EscenceCollector player;

    //Animacion
    private Animator anim;
    private SpriteRenderer spriteRenderer; //Renderizado color

    //Inmortalidad y escudo
    public int inmortal = 0;
    public int shield = 1;

    //Tiempo y muerte
    public float currentTime;
    public event Action OnDeath; //Muerte del jugador como evento.
    

    //Vida texto e imagen
    public Text Vida; //Texto del canvas que implica la vida (estadisticas UI)
    public Image lifeImage; //Imagen barra

    //Color al recibir da�o.
    public Color damageColor = Color.red;
    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
      //  player = GetComponent<EscenceCollector>();
        anim =GetComponent<Animator>();
    }

    private void Update()
    {
        maxHealth=Estadisticas.Instance.vida;

        currentTime += Time.deltaTime;
        //Daño
        if (currentHealth <= 0)
        {
            //PointManager.Instance.SaveFinalScore();
            Die();    
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            
        }

        if (inmortal==1&&currentTime >=6)
        {
            inmortal = 0;
        }

        if (shield == 2 && currentTime >= 4)
        {
            shield = 1;
        }

        lifeImage.fillAmount = currentHealth / maxHealth;
        Vida.text = "Vida " + currentHealth;
    }

    //Recibie da�o
    public void GetDamage(int value)
    {
        if(inmortal==0)
        {
        //  AudioManager.instance.PlaySound(1);
          currentHealth -= value/shield; //currentHealth = currentHealth - value;
          spriteRenderer.color = damageColor;
          Invoke("RestoreColor", 0.5f);
          //player.RestarPuntos();        ////MARTES IMPORTANTE///.
        }
    }

    private void RestoreColor()
    {
        // Restaurar el color original del sprite
        spriteRenderer.color = originalColor;
    }

    //Muere
    public void Die()
    {
       // AudioManager.instance.PlaySound(2);
        anim.SetTrigger("Death");
        Destroy(gameObject,1f);
        OnDeath?.Invoke();
    }
}
