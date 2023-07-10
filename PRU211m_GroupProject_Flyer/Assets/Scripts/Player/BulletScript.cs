using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    Rigidbody2D rigidbody;
    // audio
    [SerializeField] AudioClip hitSoundClip;

    ScoreScript scoreManager;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        scoreManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreScript>();
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
            scoreManager.AddScore(1);
            // explostion effect
            GameObject ps = ParticleSystemPool.instance.GetPooledParticleSystem();
            ps.transform.position = transform.position;
            ps.transform.rotation = Quaternion.identity;
            ParticleSystemPool.instance.EnableObject(ps);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            AudioPooling.audioInstance.PlaySound(hitSoundClip);
            gameObject.SetActive(false);
        }
    }

}
