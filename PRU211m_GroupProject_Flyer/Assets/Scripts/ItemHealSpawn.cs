using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealSpawn : MonoBehaviour
{
    [SerializeField] GameObject Heal;
    [SerializeField] float itemHealThreshold = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float randomValueSpider = Random.value; //calculate the likelihood of generating a heal item

        if (collision.gameObject.CompareTag("Bullet"))
        {
            if(randomValueSpider <= itemHealThreshold)
            {
                Instantiate(Heal, transform.position, transform.rotation);
                //BulletPooling.instance.DisableObject(Heal);
            }
            Destroy(gameObject);
        }
    }

     
}
