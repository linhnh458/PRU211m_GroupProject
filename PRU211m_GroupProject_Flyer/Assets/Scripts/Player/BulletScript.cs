using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    Rigidbody2D rigidbody;

    // audio
    [SerializeField] AudioClip hitSoundClip;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
   
    void Update()
    {
        rigidbody.velocity = transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            AudioPooling.audioInstance.PlaySound(hitSoundClip);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Frog") || collision.gameObject.CompareTag("Spider"))
        {
            AudioPooling.audioInstance.PlaySound(hitSoundClip);
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }

}
