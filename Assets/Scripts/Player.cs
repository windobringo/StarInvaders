using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PA.WeaponSystem;
using PA.HealthSystem;

public class Player : MonoBehaviour
{
    public float speed = 2;

    public GameObject shield;

    private Projectile projectile;

    public float fireRate = 1f;
    private bool isShieldActive = false;

    public Transform playerShip;

    public ScreenBounds screenBounds;

    public int initialHealthValue = 3;

    [SerializeField]
    private Transform liveImagesUIParent;
    List<Image> lives = new List<Image>();

    Rigidbody2D rb2d;
    Vector2 movementVector = Vector2.zero;

    //public AudioClip hitClip, deathClip;
    //public AudioSource hitSource;

    //public GameObject explosionFX;

    public bool isAlive = true;

    public InGameMenu loseScreen;
    public Button menuButton;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private Health health;
	private void OnEnable()
	{
		if(health == null)
		{
            health = GetComponent<Health>();
            health.InitializeHealth(initialHealthValue);
		}
        health.OnDeath.AddListener(Death);
        health.OnDeath.AddListener(UpdateUI);
        health.OnHit.AddListener(UpdateUI);
        
	}

	private void OnDisable()
	{
        health.OnDeath.RemoveListener(Death);
        health.OnDeath.RemoveListener(UpdateUI);
        health.OnHit.RemoveListener(UpdateUI);
        
	}

    private void Awake()
    {
        
        rb2d = GetComponent<Rigidbody2D>();

        foreach (Transform item in liveImagesUIParent)
        {
            lives.Add(item.GetComponent<Image>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        //get input and move
        //Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //input.Normalize();
        //movementVector = speed * input;

        PlayerMovement();

        if (isAlive == false)
            return;

        //shooting
        if (Input.GetKey(KeyCode.Space))
        {
            weapon.PerformAttack();
        }
		if (Input.GetKey(KeyCode.Q))
		{
            weapon.SwapWeapon();
		}
    }

    private void PlayerMovement()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.y -= 1; 
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1; 
        }

        input.Normalize();
        movementVector = speed * input;

        float rotationSpeed = 225f;
        
        float rotation = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation = 1; 
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation = -1; 
        }

        
        transform.Rotate(0, 0, rotation * rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb2d.position + movementVector * Time.fixedDeltaTime;
        if (screenBounds.AmIOutOfBounds(newPosition) == false)
        {
            rb2d.MovePosition(newPosition);
            //transform.Translate(tempPosition - transform.position);
        }
    }


    public void ReduceLives()
    {
        health.GetHit(1, gameObject);
    }

	

	private void Death()
	{
		isAlive = false;
		GetComponent<Collider2D>().enabled = false;
		GetComponentInChildren<SpriteRenderer>().enabled = false;
		StartCoroutine(DestroyCoroutine());
	}

	private void UpdateUI()
	{
		for (int i = 0; i < lives.Count; i++)
		{
			if (i >= health.CurrentHealth)
			{
				lives[i].color = Color.black;
			}
			else
			{
				lives[i].color = Color.white;
			}

		}
	}

	private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        loseScreen.Toggle();
        menuButton.interactable = false;
    }

    //powerup codes
    public IEnumerator SpeedBoost(float multiplier, float duration)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed /= multiplier;
    }

    // Shield Activation Coroutine
    public IEnumerator ActivateShield(float duration)
    {
        IHittable hittable = GetComponent<IHittable>();
        if (isShieldActive == true)
            hittable.GetHit(0, gameObject);

        // Here you can add shield visuals or effects
        yield return new WaitForSeconds(duration);
        isShieldActive = false;
        shield.SetActive(false);
            
    }

    public bool isFast;

    // Double Fire Rate Coroutine
    public IEnumerator DoubleFireRate(float multiplier, float duration)
    {
        isFast = true;
        fireRate *= multiplier;
        
        yield return new WaitForSeconds(duration);
        fireRate /= multiplier;
    }

}
