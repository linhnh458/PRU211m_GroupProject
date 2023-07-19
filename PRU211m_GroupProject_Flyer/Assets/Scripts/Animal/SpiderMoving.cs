using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMoving : MonoBehaviour
{
    public float verticalSpeed = 2f;    
    public float topY = 5f;             
    public float bottomY = -5f;         

    private Vector3 startPosition;
    private bool movingUp = false;

    void Start()
    {
        startPosition = transform.position;  
    }

    void Update()
    {

        if (movingUp)
        {
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);

            if (transform.position.y > topY)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);

            if (transform.position.y < bottomY)
            {
                movingUp = true;
            }
        }

        if (transform.position.x > Screen.width)
        {
            transform.position = startPosition;
        }
    }

}
