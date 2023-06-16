using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public GameObject TotalPipe;
    float spawnRate = 3f;
    float timer = 0.0f;
    float heightOffset = 3f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPine();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnPine();
            timer = 0;
        }

    }

    void spawnPine()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(TotalPipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

    }
}
