using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    Text text;
    public static int ammoAmount;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        ammoAmount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (ammoAmount > 15)
        {
            ammoAmount = 15;
            text.text = "15/15";
        }
        else if (ammoAmount > 0)
        {
            text.text = "" + ammoAmount + "/15";
        }
        else
        {
            text.text = "Out of Ammo!";
        }
    }
}

