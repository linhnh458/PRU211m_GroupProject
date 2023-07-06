using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;

    public GameObject bulletBoxPrefab;
    public int maxBulletBoxes = 10;
    public float boxSpawnInterval = 3f;
    public float boxSpeed = 5f;

    private List<GameObject> bulletBoxes = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < maxBulletBoxes; i++)
        {
            GameObject bulletBox = Instantiate(bulletBoxPrefab);
            bulletBox.SetActive(false);
            bulletBoxes.Add(bulletBox);
        }

        StartCoroutine(SpawnBulletBoxes());
    }

    private IEnumerator SpawnBulletBoxes()
    {
        while (true)
        {
            yield return new WaitForSeconds(boxSpawnInterval);
            GameObject bulletBox = GetPooledBulletBox();
            if (bulletBox != null)
            {
                bulletBox.transform.position = new Vector3(20f, Random.Range(-5f, 5f), 0f);
                bulletBox.SetActive(true);
            }
        }
    }

    public GameObject GetPooledBulletBox()
    {
        for (int i = 0; i < bulletBoxes.Count; i++)
        {
            if (!bulletBoxes[i].activeInHierarchy)
                return bulletBoxes[i];
        }
        return null;
    }

    private void Update()
    {
        // Di chuy?n h?p ??n v� ki?m tra khi n�o h?p ??n ra kh?i m�n h�nh ?? ?n ?i
        for (int i = 0; i < bulletBoxes.Count; i++)
        {
            if (bulletBoxes[i].activeInHierarchy)
            {
                bulletBoxes[i].transform.Translate(Vector3.left * boxSpeed * Time.deltaTime);
                if (bulletBoxes[i].transform.position.x < -20f)
                {
                    bulletBoxes[i].SetActive(false);
                }
            }
        }
    }

}
