using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling instance;

    public static List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] int poolSize = 7;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float disableTime = 3f; // disable bullet after x seconds
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // create a pool of inactive bullet objects
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if object is inactive => available to use
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    // Disable bullet
    public void DisableObject(GameObject obj)
    {
        StartCoroutine(DisableAfterTime(obj));
    }

    private IEnumerator DisableAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(disableTime);
        obj.SetActive(false);
    }
}
