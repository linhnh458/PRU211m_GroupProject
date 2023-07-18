using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingForBoss : MonoBehaviour
{
    public static ObjectPoolingForBoss instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    //private int amountToPool = 12;

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
        for (int i = 0; i < 2; i++)
        {
            GameObject boss = Instantiate(Boss);
            boss.SetActive(false);
            pooledObjects.Add(boss);
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
