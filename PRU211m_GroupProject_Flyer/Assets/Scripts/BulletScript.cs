using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletLifetime = 4f;
    Rigidbody2D rigidbody;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigidbody.velocity = transform.up * bulletSpeed;
        Destroy(gameObject, bulletLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }*/
        if (collision.gameObject.CompareTag("Frog") || collision.gameObject.CompareTag("Spider"))
        {
            Destroy(collision.gameObject);
        }
    }
}
