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
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("Hit pipe");
        }*/
    }

    private void Flicker(GameObject gameObject)
    {
        Debug.Log("Player just hit an animal");
    }
}
