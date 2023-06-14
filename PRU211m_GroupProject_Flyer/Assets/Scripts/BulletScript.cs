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
        Destroy(gameObject, bulletLifetime);
    }
    void Update()
    {
        rigidbody.velocity = transform.up * bulletSpeed;
    }
}
