using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public float moveSpeed = 0.01f;
    float deadZone = -20;
    private bool isPaused = false;
    private Vector3 velocity;
    float speedUpRate = 30f;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            Move();
        }
    }

    void Move()
    {
        transform.position += Vector3.left * moveSpeed;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
        if (timer < speedUpRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            moveSpeed = moveSpeed + moveSpeed * 0.2f;
            timer = 0;
        }
    }

    public void Pause()
    {
        isPaused = true;
        velocity = Vector3.zero;
    }

    public void Resume()
    {
        isPaused = false;
        velocity = Vector3.left * moveSpeed;
    }
}
