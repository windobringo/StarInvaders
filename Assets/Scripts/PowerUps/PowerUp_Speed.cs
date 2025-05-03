using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Speed : MonoBehaviour, IPowerUp
{
    public float speedMultiplier = 0.5f;
    public float duration = 5f;

    private Rigidbody2D rb2d;

    public float speed = 4;
    public float speedVariation = 1f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed += UnityEngine.Random.Range(0, speedVariation);
    }
    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + Vector2.down * speed * Time.deltaTime);
    }

    public void ApplyPowerUp(GameObject player)
    {
        Player controller = player.GetComponent<Player>();
        if (controller != null)
        {
            controller.StartCoroutine(controller.SpeedBoost(speedMultiplier, duration));
        }
    }
}
