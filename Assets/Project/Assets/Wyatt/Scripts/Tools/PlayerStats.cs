﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour 
{
    [Header("Setup")]
    public GameObject spawnPoint;
    public Gun gun; // THIS IS JUST FOR RESETTING TEXT ON DEATH
    public WeaponSwitch weapons;
    public bool inMenu;

    [Header("UI Elements")]
    public Text scoreText;
    public Text livesText;
    public Text healthText;
    public Text armorText;
    public Text ammoText;

    [Header("Stats")]
    public float Armor;
    private float startArmor = 50f;
    public float Health;
    private float startHealth = 100f;
    public int Lives;
    public float Score;

    void Start()
    {
        scoreText.text = Score.ToString();
        livesText.text = Lives.ToString();
        if (inMenu == true)
        {
            Health = 1000000;
        }
        else
        {
            Health = startHealth;
        }
        healthText.text = Health.ToString();
        Armor = startArmor;
        armorText.text = Armor.ToString();
        ammoText.text = "NA";
    }

    public void TakeDamage(float amount)
    {
        Armor -= amount / 2f;
        armorText.text = Mathf.RoundToInt(Armor).ToString();

        float damageAfterArmor = Armor - amount ;
        Health -= damageAfterArmor;
        healthText.text = Mathf.RoundToInt(Health).ToString();
        if (Health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        weapons.selectedWeapon = 1;
        Health = startHealth;
        healthText.text = Mathf.RoundToInt(Health).ToString();
        armorText.text = Mathf.RoundToInt(Armor).ToString();
        ammoText.text = gun.currentGunAmmoMG.ToString() + " / " + gun.totalAmmoMG;
        Lives--;
        livesText.text = Lives.ToString();
        gameObject.transform.position = spawnPoint.transform.position;
        gameObject.transform.rotation = spawnPoint.transform.rotation;
    }

    void GameOver()
    {
        if(Lives <= 0)
        {
            SceneManager.LoadScene("NewTestScene");  // Replace when adding game over screen
        }
    }
}
