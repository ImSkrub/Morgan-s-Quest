using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int lifeBonus = 25; // Amount of life to give to the player
    
    [SerializeField] private ParticleSystem pickupParticles;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with the power-up
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a PlayerHealth script to manage health
            LifePlayer playerHealth = other.GetComponent<LifePlayer>();
            if (playerHealth != null)
            {
                playerHealth.AddLife(lifeBonus); // Add life to the player
                Debug.Log("Power-up collected! Life increased by " + lifeBonus);
            }

            if (pickupParticles != null)
            {
                Instantiate(pickupParticles, transform.position, Quaternion.identity);
            }

            // Destroy the power-up after collection
            Destroy(gameObject);
        }
    }
}
