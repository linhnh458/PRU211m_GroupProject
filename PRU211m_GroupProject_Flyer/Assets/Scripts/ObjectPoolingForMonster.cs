using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolingForMonster : MonoBehaviour
{
    public static ObjectPoolingForMonster instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    //private int amountToPool = 12;

    [SerializeField]
    private GameObject Frog;
    [SerializeField]
    private GameObject Spider;
    [SerializeField]
    private GameObject Boss;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject frog = Instantiate(Frog);
            frog.SetActive(false);
            pooledObjects.Add(frog);
        }for (int i = 0; i < 5; i++)
        {
            GameObject spider = Instantiate(Spider);
            spider.SetActive(false);
            pooledObjects.Add(spider);
        }for (int i = 0; i < 2; i++)
        {
            GameObject boss = Instantiate(Boss);
            boss.SetActive(false);
            pooledObjects.Add(boss);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!(pooledObjects[i].activeInHierarchy))
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
