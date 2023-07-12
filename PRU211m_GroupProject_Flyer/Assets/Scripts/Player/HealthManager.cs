using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int currentHeart; // current number of hearts player has
    [SerializeField] int maxHeart; // maximum hearts can have
    [SerializeField] Image[] hearts;
    public Sprite fullHeart;
    public Button pauseButton;

    private void Update()
    {
        // check whether health is greater than maximum allowed 
        if (currentHeart > maxHeart)
        {
            currentHeart = maxHeart; // cannot exceed the max num of hearts
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            // display current number of hearts that player has
            if (i < currentHeart)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHeart -= damageAmount;
        if (currentHeart > 0)
        {
            PlayerScript.isGameOver = false;
        }

        if (currentHeart <= 0)
        {
            currentHeart = 0;
            //PlayerScript.isGameOver = true;
            //pauseButton.gameObject.SetActive(false);
        }
        else
        {
            PlayerScript.isGameOver = false;
        }
    }

    public void Die()
    {
        currentHeart = 0;
        //PlayerScript.isGameOver = true;
        //pauseButton.gameObject.SetActive(false);
    }

    public void HealHealth(int heal)
    {
        currentHeart += heal;

        if (currentHeart > maxHeart)
        {
            currentHeart = maxHeart;
        }
    }
}
