using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 10f;

    // sniper to shoot bullets
    [SerializeField][Range(0.1f, 1f)] float firingRate = 0.5f;
    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject bulletPrefab;
    float fireTimer; // when to shoot
    // audio
    [SerializeField] AudioSource hitSoundSource;
    [SerializeField] AudioClip hitSoundClip;
    [SerializeField] AudioClip deadSoundClip;
    // blinking effect
    [SerializeField] float blinkDuration = 1.5f; // Duration of blinking in seconds
    [SerializeField] float blinkInterval = 0.3f; // Interval between blink toggles
    private SpriteRenderer rend;

    [SerializeField] Joystick joystick;
    public static bool isGameOver = false;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] Button pauseButton;
    public static object Instance { get; internal set; }
    private void Awake()
    {
        gameOverMenu.SetActive(false);
    }
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement;
        if (joystick.Vertical >= .2f || joystick.Vertical <= -.2f) // When the joystick is moved to the circle border, this code will run
        {
            movement = joystick.Vertical;
        }
        else
        {
            movement = 0f;
        }
        rigidbody2d.velocity = new Vector2(0, movement) * speed;
        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0)
        {
            Shoot();
            fireTimer = firingRate; // fire bullet every x seconds
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
        if (isGameOver)
        {
            gameOverMenu.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
    }

    public void Shoot()
    {
        if (AmmoText.ammoAmount > 0)
        {
            AmmoText.ammoAmount -= 1;
            // Bắn đạn và giảm số đạn
            GameObject bullet = BulletPooling.instance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = firingPoint.position;
                bullet.transform.rotation = firingPoint.rotation;
                bullet.SetActive(true);
                BulletPooling.instance.DisableObject(bullet);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            GetComponent<HealthManager>().Die();
            AudioPooling.audioInstance.PlaySound(deadSoundClip);
            // spawn exlposion 
            SpawnExplosionEffect();
        }
        else if (collision.gameObject.CompareTag("PartOfFence"))
        {
            GetComponent<HealthManager>().Die();
            AudioPooling.audioInstance.PlaySound(deadSoundClip);
            // spawn exlposion 
            SpawnExplosionEffect();
        }
        else if (collision.gameObject.CompareTag("Frog") || collision.gameObject.CompareTag("Spider"))
        {
            GetComponent<HealthManager>().TakeDamage(1); // lose a heart
            AudioPooling.audioInstance.PlaySound(hitSoundClip);
            StartCoroutine(Blink());
            collision.gameObject.SetActive(false); // kill animal
            // spawn exlposion 
            SpawnExplosionEffect();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            GetComponent<HealthManager>().Die(); 
            AudioPooling.audioInstance.PlaySound(deadSoundClip);
            // spawn exlposion 
            SpawnExplosionEffect();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Heal"))
        {
            GetComponent<HealthManager>().HealHealth(1); // get a heart
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Poison"))
        {
            GetComponent<HealthManager>().TakeDamage(1); // lose a heart
            AudioPooling.audioInstance.PlaySound(hitSoundClip);
            StartCoroutine(Blink());
            collision.gameObject.SetActive(false); // disable boss bullet
            SpawnExplosionEffect();
        }
    }

    IEnumerator Blink()
    {
        // Calculate the number of blink toggles based on duration and interval
        int toggleCount = Mathf.RoundToInt(blinkDuration / blinkInterval);
        for (int i = 0; i < toggleCount; i++)
        {
            // Toggle the visibility state
            rend.enabled = !rend.enabled;
            // Wait for x seconds then toggle again
            yield return new WaitForSeconds(blinkInterval);
        }
        // Restore the initial visibility state after blinking is done
        rend.enabled = true;
    }

    void SpawnExplosionEffect()
    {
        GameObject effect = ParticleSystemPool.instance.GetPooledParticleSystem();
        //explosion.tag = "";
        effect.transform.position = transform.position;
        effect.transform.rotation = Quaternion.identity;
        ParticleSystemPool.instance.EnableObject(effect);
    }
}
