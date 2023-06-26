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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Frog") || collision.gameObject.CompareTag("Spider"))
        {
            //collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    // OnEnable is called every time the object is enabled
    private void OnEnable()
    {
        // wait for x seconds before disable the bullet
        StartCoroutine(WaitToDisable(bulletLifetime));
    }

    private IEnumerator WaitToDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
