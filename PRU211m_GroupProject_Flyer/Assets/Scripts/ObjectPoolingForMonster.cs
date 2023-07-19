using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolingForMonster : MonoBehaviour
{
    public static ObjectPoolingForMonster instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 12;

    [SerializeField]
    private GameObject Frog;

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
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject frog = Instantiate(Frog);
            frog.SetActive(false);
            pooledObjects.Add(frog);
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
    /*private IEnumerator PlaceObjectAtRandomPositionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Ch? 2 giây

            GameObject obj = GetPooledObject();

            if (obj != null)
            {
                // L?y ng?u nhiên v? trí trong kho?ng minPosition và maxPosition
                float randomX = Random.Range(transform.position.x - 2, transform.position.x + 1);
                float randomY = 5;
                Vector2 randomPosition = new Vector2(randomX, randomY);

                // ??t ??i t??ng vào v? trí ng?u nhiên và kích ho?t
                obj.transform.position = randomPosition;
                obj.SetActive(true);
            }
        }
    }*/
}
