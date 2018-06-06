using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGunPickup : MonoBehaviour
{
    public WeaponSwitch weapons;
    public AudioSource gunPickup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            weapons.hasPickedUpGun = true;
            gunPickup.Play();
            weapons.SelectWeapon();
            Destroy(gameObject);
        }
    }
}
