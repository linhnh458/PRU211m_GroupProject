﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 5; // Số lượng đạn cung cấp cho Player khi chạm vào hộp đạn

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerScript player = collision.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.AddAmmo(ammoAmount);
            }
            gameObject.SetActive(false); // Ẩn hộp đạn
        }
    }


}
