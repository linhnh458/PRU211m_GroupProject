using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField] float speed = 10f;
    void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxisRaw("Vertical");
        rigidbody.velocity = new Vector3(0, movement, 0) * speed;
    }
}
