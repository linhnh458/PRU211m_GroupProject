using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.01f;
    float deadZone = -20;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}