using System;
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
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxisRaw("Vertical");
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

    void Shoot()
    {
        // shoot bullet right at the position of firing point
        GameObject bullet = BulletPooling.instance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = firingPoint.position;
            bullet.transform.rotation = firingPoint.rotation;
            bullet.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            GetComponent<HealthManager>().TakeDamage(1); // lose a heart
            AudioPooling.audioInstance.PlaySound(deadSoundClip);
            Debug.Log("Hit pipe");
        }
        if (collision.gameObject.CompareTag("Frog") || collision.gameObject.CompareTag("Spider"))
        {
            GetComponent<HealthManager>().TakeDamage(1); // lose a heart
            AudioPooling.audioInstance.PlaySound(hitSoundClip); 
            collision.gameObject.SetActive(false); // kill animal
        }
    }

}
