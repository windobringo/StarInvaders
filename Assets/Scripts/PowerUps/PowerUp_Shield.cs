using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Shield : MonoBehaviour, IPowerUp
{
    public float duration = 10f;
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
        Player player1 = player.GetComponent<Player>();
        player1.shield.SetActive(true);
        if (player1 != null)
        {
            player1.StartCoroutine(player1.ActivateShield(duration));
        }
    }
}
