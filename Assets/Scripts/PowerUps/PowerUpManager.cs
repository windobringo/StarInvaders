using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IPowerUp powerUp = GetComponent<IPowerUp>();
            if (powerUp != null)
            {
                powerUp.ApplyPowerUp(other.gameObject);
                Destroy(gameObject);  // Destroy the power-up after use
            }
        }
    }
}
