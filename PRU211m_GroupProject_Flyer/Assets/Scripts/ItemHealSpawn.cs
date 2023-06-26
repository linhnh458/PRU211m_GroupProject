using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject Heal;
    float itemHealThreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float randomValueSpider = Random.value;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            if(randomValueSpider <= itemHealThreshold)
            {
                Instantiate(Heal, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
