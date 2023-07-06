﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rigidbody;
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
    private bool isBlinking = false;

    [SerializeField] Joystick joystick;
    [SerializeField] int currentAmmo = 10, maxAmmo = 15;

    public static object Instance { get; internal set; }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        rigidbody.velocity = new Vector2(0, movement) * speed;
        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0)
        {
            Shoot();
            fireTimer = firingRate; // fire bullet every x seconds
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (currentAmmo > 0)
        {
            // Bắn đạn và giảm số đạn
            GameObject bullet = BulletPooling.instance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = firingPoint.position;
                bullet.transform.rotation = firingPoint.rotation;
                bullet.SetActive(true);
                BulletPooling.instance.DisableObject(bullet);
            }
            currentAmmo--;
        }


/*        // shoot bullet right at the position of firing point
        GameObject bullet = BulletPooling.instance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = firingPoint.position;
            bullet.transform.rotation = firingPoint.rotation;
            bullet.SetActive(true);
            BulletPooling.instance.DisableObject(bullet);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            GetComponent<HealthManager>().TakeDamage(1); // lose a heart
            AudioPooling.audioInstance.PlaySound(deadSoundClip);
        }
        else if (collision.gameObject.CompareTag("Frog") || collision.gameObject.CompareTag("Spider"))
        {
            GetComponent<HealthManager>().TakeDamage(1); // lose a heart
            AudioPooling.audioInstance.PlaySound(hitSoundClip);
            StartCoroutine(Blink());
            collision.gameObject.SetActive(false); // kill animal
            // spawn exlposion 
            GameObject ps = ParticleSystemPool.instance.GetPooledParticleSystem();
            ps.transform.position = transform.position;
            ps.transform.rotation = Quaternion.identity;
            ParticleSystemPool.instance.EnableObject(ps);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            GetComponent<HealthManager>().TakeDamage(1); // lose a heart
            AudioPooling.audioInstance.PlaySound(deadSoundClip);
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
            GameObject ps = ParticleSystemPool.instance.GetPooledParticleSystem();
            ps.transform.position = transform.position;
            ps.transform.rotation = Quaternion.identity;
            ParticleSystemPool.instance.EnableObject(ps);
        }
    }

    IEnumerator Blink()
    {
        // Store the initial visibility state of the object
        bool initialState = rend.enabled;
        // Calculate the number of blink toggles based on duration and interval
        int toggleCount = Mathf.RoundToInt(blinkDuration / blinkInterval);
        for (int i = 0; i < toggleCount; i++)
        {
            // Toggle the visibility state of the object
            rend.enabled = !rend.enabled;

            // Wait for the specified interval
            yield return new WaitForSeconds(blinkInterval);
        }
        // Restore the initial visibility state after blinking is done
        rend.enabled = initialState;
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        if (currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;
    }

}
