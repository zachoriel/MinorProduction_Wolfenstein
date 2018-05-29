using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PlayerStats player;
    public GameObject pickupUI;
    private bool isEnabled;

	// Use this for initialization
	void Start ()
    {
        isEnabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "Armor")
            {
                AddArmor();
                isEnabled = false;
                pickupUI.SetActive(false);
                Destroy(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "Health")
            {
                AddHealth();
                isEnabled = false;
                pickupUI.SetActive(false);
                Destroy(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "Life")
            {
                AddLife();
                isEnabled = false;
                pickupUI.SetActive(false);
                Destroy(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "Ammo")
            {
                AddAmmo();
                isEnabled = false;
                pickupUI.SetActive(false);
                Destroy(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "BigAmmo")
            {
                RestoreAmmo();
                isEnabled = false;
                pickupUI.SetActive(false);
                Destroy(gameObject);
            }
        }
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.rigidbody)
        {
            PickupUI();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.rigidbody)
        {
            pickupUI.SetActive(false);
        }
    }

    void PickupUI()
    {
        pickupUI.SetActive(true);
        isEnabled = true;
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
