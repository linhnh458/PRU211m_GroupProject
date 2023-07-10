using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPool : MonoBehaviour
{
    public static ParticleSystemPool instance;
    [SerializeField] GameObject explosionPrefab;
    private int poolSize = 10;
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(explosionPrefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetPooledParticleSystem()
    {
        if (objectPool.Count == 0)
        {
            Debug.LogWarning("Object pool is empty! Consider increasing the pool size.");
            return null;
        }

        GameObject obj = objectPool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void EnableObject(GameObject obj)
    {
        StartCoroutine(DisableAfterTime(obj));
    }

    // return to pool
    private IEnumerator DisableAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(3f);
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
