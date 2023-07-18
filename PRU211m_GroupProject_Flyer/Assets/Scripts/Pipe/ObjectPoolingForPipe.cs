using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingForPipe : MonoBehaviour
{
    public static ObjectPoolingForPipe instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    //private int amountToPool = 12;

    [SerializeField]
    private GameObject Pipe;

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
            GameObject pipe = Instantiate(Pipe);
            pipe.SetActive(false);
            pooledObjects.Add(pipe);
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
