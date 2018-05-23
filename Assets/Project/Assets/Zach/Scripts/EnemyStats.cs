using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public PlayerStats playerStats;

    public float health;
    public float startHealth = 100f;

    public Image healthBar;

    void Start()
    {
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0f)
        {
            Die(); 
        }
    }

    void Die()
    {
        playerStats.Score += 10;
        playerStats.scoreText.text = playerStats.Score.ToString();

        if (playerStats.Score % 500 == 0)
        {
            playerStats.Lives++;
        }

        Destroy(gameObject);
    }
}
