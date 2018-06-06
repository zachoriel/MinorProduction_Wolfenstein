using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour 
{
    [Header("Setup")]
    public GameObject spawnPoint;
    public Gun gun; // THIS IS JUST FOR RESETTING TEXT ON DEATH
    public WeaponSwitch weapons;

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
    //public int maxGunAmmo = 25;
    //public int currentGunAmmo;
    //public int totalAmmo = 100;
    public int Lives;
    public float Score;

    void Start()
    {
        scoreText.text = Score.ToString();
        livesText.text = Lives.ToString();
        Health = startHealth;
        healthText.text = Health.ToString();
        Armor = startArmor;
        armorText.text = Armor.ToString();
        //currentGunAmmo = maxGunAmmo;
        ammoText.text = "NA";
    }

    public void TakeDamage(float amount)
    {
        if(Armor > 0)
        {
             Armor -= amount;
            armorText.text = Mathf.RoundToInt(Armor).ToString();
        }
        else if(Armor <= 0)
        {
            Armor = 0;
            Health -= amount;
            healthText.text = Mathf.RoundToInt(Health).ToString();
            armorText.text = Mathf.RoundToInt(Armor).ToString();
        }
        if (Health < 0.5f && Health > 0f)
        {
            Health = 0f;
        }
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

    void Update()  // REMOVE WHEN YOU'RE DONE WITH AMMO FUCKER
    {
        //ammoText.text = currentGunAmmo.ToString() + " / " + totalAmmo;
    }
}
