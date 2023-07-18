using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingForSpider : MonoBehaviour
{
    public static ObjectPoolingForSpider instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    //private int amountToPool = 12;

    [SerializeField]
    private GameObject Spider;

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
            GameObject spider = Instantiate(Spider);
            spider.SetActive(false);
            pooledObjects.Add(spider);
        }
        //StartCoroutine(PlaceObjectAtRandomPositionCoroutine());
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
