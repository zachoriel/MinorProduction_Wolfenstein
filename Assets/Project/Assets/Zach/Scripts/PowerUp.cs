using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Script Setup")]
    public PlayerStats player;
    public Gun gun;
    public Shotgun shotgun;
    public LaserRifle laser;

    [Header("Audio")]
    public AudioSource ammoPickup;
    public AudioSource armorPickup;
    public AudioSource healthPickup;
    public AudioSource lifePickup;

    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera" && gameObject.tag == "Armor")
        {
            AddArmor();
            Destroy(gameObject);
        }
        else if (other.tag == "MainCamera" && gameObject.tag == "Health")
        {
            AddHealth();
            Destroy(gameObject);
        }
        else if (other.tag == "MainCamera" && gameObject.tag == "Life")
        {
            AddLife();
            Destroy(gameObject);
        }
        else if (other.tag == "MainCamera" && gameObject.tag == "Ammo")
        {
            AddAmmo();
            Destroy(gameObject);
        }
        else if (other.tag == "MainCamera" && gameObject.tag == "BigAmmo")
        {
            RestoreAmmo();
            Destroy(gameObject);
        }
    }

    void AddArmor()
    {
        armorPickup.Play();
        player.Armor += 25;
        player.armorText.text = Mathf.RoundToInt(player.Armor).ToString();
    }

    void AddHealth()
    {
        healthPickup.Play();
        player.Health += 25;
        player.healthText.text = Mathf.RoundToInt(player.Health).ToString();
    }

    void AddLife()
    {
        lifePickup.Play(); 
        player.Lives += 1;
        player.livesText.text = Mathf.RoundToInt(player.Lives).ToString();
    }

    void AddAmmo()
    {
        ammoPickup.Play();
        gun.currentGunAmmoMG = gun.maxGunAmmoMG;
        shotgun.currentGunAmmoSG = shotgun.maxGunAmmoSG;
        laser.energy = laser.maxEnergy;
    }

    void RestoreAmmo()
    {
        ammoPickup.Play();
        gun.currentGunAmmoMG = gun.maxGunAmmoMG;
        gun.totalAmmoMG = 100;
        shotgun.currentGunAmmoSG = shotgun.maxGunAmmoSG;
        shotgun.totalAmmoSG = 30;
        laser.energy = laser.maxEnergy;
    }
}
