﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Script Setup")]
    public Win winCondition;
    public PlayerStats player;

    [Header("UI Elements")]
    public GameObject winUI;

    private bool isEnabled;
    private float range;

	// Use this for initialization
	void Start ()
    {
        isEnabled = false;
	} 
	
	// Update is called once per frame
	void Update ()
    {
        range = Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position);

        if (range <= 10 && gameObject.tag == "WinItem" && winCondition.enemies.Length == 0)
        {
            WinUI();
        }
        else if (range > 10 && gameObject.tag == "WinItem" && winCondition.enemies.Length > 0)
        {
            CloseWinUI();
        }
        else if (range > 10 && gameObject.tag == "WinItem" && winCondition.enemies.Length == 0)
        {
            CloseWinUI();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Armor")
        {
            AddArmor();
            Destroy(gameObject);
        }
        else if (other.tag == "Player" && gameObject.tag == "Health")
        {
            AddHealth();
            Destroy(gameObject);
        }
        else if (other.tag == "Player" && gameObject.tag == "Life")
        {
            AddLife();
            Destroy(gameObject);
        }
        else if (other.tag == "Player" && gameObject.tag == "Ammo")
        {
            AddAmmo();
            Destroy(gameObject);
        }
        else if (other.tag == "Player" && gameObject.tag == "BigAmmo")
        {
            RestoreAmmo();
            Destroy(gameObject);
        }
    }

    void WinUI()
    {
        winUI.SetActive(true);
        isEnabled = true;
    }
    void CloseWinUI()
    {
        winUI.SetActive(false);
        isEnabled = false;
    }

    void AddArmor()
    {
        player.Armor += 25;
        player.armorText.text = Mathf.RoundToInt(player.Armor).ToString();
    }

    void AddHealth()
    {
        player.Health += 25;
        player.healthText.text = Mathf.RoundToInt(player.Health).ToString();
    }

    void AddLife()
    {
        player.Lives += 1;
        player.livesText.text = Mathf.RoundToInt(player.Lives).ToString();
    }

    void AddAmmo()
    {
        player.currentGunAmmo = player.maxGunAmmo;
        player.ammoText.text = Mathf.RoundToInt(player.currentGunAmmo).ToString() + " / " + player.totalAmmo;
    }

    void RestoreAmmo()
    {
        player.currentGunAmmo = player.maxGunAmmo;
        player.totalAmmo = 100;
        player.ammoText.text = Mathf.RoundToInt(player.currentGunAmmo).ToString() + " / " + player.totalAmmo;
    }
}
