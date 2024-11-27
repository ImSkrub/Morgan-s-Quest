using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaPlayer : MonoBehaviour
{
    //Variables mana
    public int maxMana; //Maximo de mana
    public float currentMana; //Mana in game
    public float manaCooldown = 1f; //Daño

    [SerializeField][Range(0,5)] float maxTime;
    private float currentTime;

   
    public Image manaImage; //Imagen barra

    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime*2;
        if (currentMana < maxMana)
        {
            OffMana();
        }

        manaImage.fillAmount = currentMana / maxMana;
    }
    private void OffMana()
    {
        //Temporizador
        if(currentTime > maxTime)
        {
            currentMana+=10;
            currentTime = 0;
        }
    }
}
