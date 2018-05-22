using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public EnemyGun gun;

    public float health;
    public float startHealth = 100f;

    void Start()
    {
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("TestingScene");
    }
}
