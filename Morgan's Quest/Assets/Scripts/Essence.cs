using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    [SerializeField] public int value;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && EssenceManager.instance.escenceStack != null)
        {
            GameManager.Instance.escence += value;
            EssenceManager.instance.escenceStack.Push(this);
            Destroy(gameObject);
        }
    }
}
