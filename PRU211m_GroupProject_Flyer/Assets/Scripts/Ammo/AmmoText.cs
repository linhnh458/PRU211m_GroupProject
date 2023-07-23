using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    Text ammoTextDisplay;
    public static int ammoAmount = 5;
    private int maxAmmo = 15;

    void Start()
    {
        ammoTextDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ammoAmount > 0)
        {
            if (ammoAmount > maxAmmo)
            {
                ammoAmount = maxAmmo;
            }
            ammoTextDisplay.text = "Ammo: "+ ammoAmount + "/" + maxAmmo.ToString();
        }
        else
        {
            ammoTextDisplay.text = "Out of Ammo!";
        }
    }

    //void updateAmmo()
    //{
    //    if (ammoAmount > maxAmmo)
    //    {
    //        ammoAmount = maxAmmo;
    //        text.text = ammoAmount.ToString() + "/" + maxAmmo.ToString();
    //    }
    //    else if (ammoAmount > 0)
    //    {
    //        Debug.Log("Current ammo: " + ammoAmount);
    //        text.text = ammoAmount + "/" + maxAmmo.ToString();
    //    }
    //    else
    //    {
    //        text.text = "Out of Ammo!";
    //    }
    //}
}

