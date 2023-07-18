using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    Rigidbody2D rigidbody2d;
    // audio
    [SerializeField] AudioClip hitSoundClip;

    ScoreScript scoreManager;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        scoreManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreScript>();
    }
   
    void Update()
    {
        rigidbody2d.velocity = transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Boss"))
        {
            AudioPooling.audioInstance.PlaySound(hitSoundClip);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Frog") || collision.gameObject.CompareTag("Spider"))
        {
            //AudioPooling.audioInstance.PlaySound(hitSoundClip);
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            scoreManager.AddScore(1);
            SpawnExplosionEffect();
        }
        if (collision.gameObject.CompareTag("PartOfFence"))
        {
            //AudioPooling.audioInstance.PlaySound(hitSoundClip);
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            SpawnExplosionEffect();
        }
    }


    void SpawnExplosionEffect()
    {
        GameObject effect = ParticleSystemPool.instance.GetPooledParticleSystem();
        effect.transform.position = transform.position;
        effect.transform.rotation = Quaternion.identity;
        ParticleSystemPool.instance.EnableObject(effect);
    }
}
