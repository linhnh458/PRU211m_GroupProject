using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolingAmmo : MonoBehaviour
{
    public static ObjectPoolingAmmo instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    //private int amountToPool = 12;

    [SerializeField]
    private GameObject Ammo;

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
            GameObject ammo = Instantiate(Ammo);
            ammo.SetActive(false);
            pooledObjects.Add(ammo);
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
