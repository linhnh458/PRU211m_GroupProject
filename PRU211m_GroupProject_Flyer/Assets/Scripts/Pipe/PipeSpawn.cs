using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    [SerializeField] GameObject TotalPipe;
    [SerializeField] GameObject frog;
    [SerializeField] GameObject spider;
    [SerializeField] GameObject boss;
    float spawnRate = 3f;
    float timer = 0.0f;
    float heightOffset = 4f;
    private float initialDelay = 2.4f;
    private float repeatDelay = 2f;

    private float spawnFrogThreshold = 0.3f;
    private float spawnSpiderThreshold = 0.3f;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPine();
        Invoke("spawnMonster", initialDelay);

        // L?p l?i vi?c spawn v?t th? sau m?i kho?ng th?i gian
        InvokeRepeating("spawnMonster", initialDelay + repeatDelay, repeatDelay);
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
    void spawnMonster()
    {
        if (count < 10)
        {
            SpawnFrog();
            SpawnSpider();
        }
        if(count >= 10)
        {
            SpawnBoss();
        }
    }
    void SpawnSpider()
    {
        float randomValueSpider = Random.value;

        if (randomValueSpider <= spawnSpiderThreshold)
        {
            Instantiate(spider, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), 5, 0), transform.rotation);
            count++;
            spawnSpiderThreshold = spawnSpiderThreshold + 0.05f;
            if (spawnSpiderThreshold >= 0.7f)
            {
                spawnSpiderThreshold = 0.7f;
            }
        }
    }
    void SpawnFrog()
    {
        float randomValueFrog = Random.value;


        if (randomValueFrog <= spawnFrogThreshold)
        {
            Instantiate(frog, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), -5, 0), transform.rotation);
            count++;
            spawnFrogThreshold = spawnFrogThreshold + 0.05f;
            if (spawnFrogThreshold >= 0.7f)
            {
                spawnFrogThreshold = 0.7f;
            }
        }
    }
    
    void SpawnBoss()
    {
        if(count >= 10)
        {
            Instantiate(boss, new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1), 0, 0), transform.rotation);
            count = 0;
        }
        
    }

}
