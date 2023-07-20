using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBulletScript : MonoBehaviour
{
    private SpriteRenderer bossBulletRenderer;
    private string playerTag = "Player";
    private Transform player;  // player's transform
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        bossBulletRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(bossBulletRenderer.name);
    }

    // Update is called once per frame
    void Update()
    {
        FlipBullet();
    }

    // change bullet direction according to player position
    void FlipBullet()
    {
        float playerPositionX = player.position.x;
        if (gameObject.transform.position.x > playerPositionX)
        {
            Debug.Log("Player on the left of bullet");
            bossBulletRenderer.flipX = false; // flip sprite when player is on the right of the bullet
        }
        else
        {
            Debug.Log("Player on the right of bullet");
            bossBulletRenderer.flipX = true; 
        }
    }
}
