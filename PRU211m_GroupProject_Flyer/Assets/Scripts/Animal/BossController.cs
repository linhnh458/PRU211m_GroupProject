using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;  // Prefab c?a viên ??n
    [SerializeField] string playerTag = "Player";  // Tag c?a ??i t??ng ng??i ch?i
    [SerializeField] float bulletSpeed = 5f;  // T?c ?? ??n

    private float fireRate = 1f;  
    private float nextFireTime = 0f;  // Th?i ?i?m b?n ??n ti?p theo
    private Transform player;  // Transform c?a ng??i ch?i
    [SerializeField] float bulletLifetime = 4f;

    ScoreScript scoreManager;
    private int hitCount = 0; // count bullets hit the boss
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        scoreManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreScript>();
    }

    private void Update()
    {
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Shoot()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        Destroy(bullet, bulletLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitCount++;
        }
        if(hitCount >= 3)
        {
            gameObject.SetActive(false);
            scoreManager.AddScore(3);
        }
    }
}
