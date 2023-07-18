using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingForFence : MonoBehaviour
{
    public static ObjectPoolingForFence instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    //private int amountToPool = 12;

    [SerializeField]
    private GameObject Fence;

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
        for (int i = 0; i < 10; i++)
        {
            GameObject fence = Instantiate(Fence);
            fence.SetActive(false);
            pooledObjects.Add(fence);
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
