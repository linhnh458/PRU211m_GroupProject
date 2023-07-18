using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    [SerializeField] GameObject TotalPipe;
    [SerializeField] GameObject Fence;
    [SerializeField] GameObject frog;
    [SerializeField] GameObject spider;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject ammo;
    float spawnRate = 2f;
    float timer = 0.0f;
    float heightOffset = 4f;
    private float initialDelay = 2.4f;
    private float repeatDelay = 2f;

    private float spawnFrogThreshold = 0.3f;
    private float spawnSpiderThreshold = 0.3f;
    private float spawnFenceThreshold = 0.3f;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
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
            spawnPipe();
            spawnAmmo();
            timer = 0;
        }
        
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        GameObject pipe = ObjectPoolingForPipe.instance.GetPooledObject();
        if (pipe != null)
        {
            pipe.transform.position = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);
            pipe.transform.rotation = transform.rotation;
        }
/*        GameObject fence = ObjectPoolingForFence.instance.GetPooledObject();
        if (fence != null)
        {
            fence.transform.position = new Vector3(transform.position.x, -2, 0);
            fence.transform.rotation = transform.rotation;
        }*/
        float randomValueFence = Random.value;

        if (randomValueFence <= spawnFenceThreshold)
        {
            //fence.SetActive(true);
            Instantiate(Fence, new Vector3(transform.position.x, -2, 0), transform.rotation);
        }
        else
        {
            pipe.SetActive(true);
        }

    }

    void spawnAmmo()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        //Instantiate(ammo, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

        GameObject ammo = ObjectPoolingAmmo.instance.GetPooledObject();
        if (ammo != null)
        {
            ammo.transform.position = new Vector3(transform.position.x, Random.Range(-5, 5), 0);
            ammo.transform.rotation = transform.rotation;
            ammo.SetActive(true);
        }
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
            GameObject spider = ObjectPoolingForSpider.instance.GetPooledObject();
            if (spider != null)
            {
                spider.transform.position = new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 1 / 2), 5, 0);
                spider.transform.rotation = transform.rotation;
                spider.SetActive(true);
            }
            //Instantiate(spider, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 1 / 2), 5, 0), transform.rotation);
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
            GameObject frog = ObjectPoolingForMonster.instance.GetPooledObject();
            if (frog != null)
            {
                frog.transform.position = new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 1 / 2), -5, 0);
                frog.transform.rotation = transform.rotation;
                frog.SetActive(true);
            }
            //Instantiate(frog, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 1 / 2), -5, 0), transform.rotation);
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
            GameObject boss = ObjectPoolingForBoss.instance.GetPooledObject();
            if (boss != null)
            {
                boss.transform.position = new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1 / 2), 0, 0);
                boss.transform.rotation = transform.rotation;
                boss.SetActive(true);
            }
            //Instantiate(boss, new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1 / 2), 0, 0), transform.rotation);
            count = 0;
        }
        
    }

}
