using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public EnemyGun gun;
    public int Lives;
    public float Score;

    public GameObject spawnPoint;



    public float armor;
    public float health;
    public float startHealth = 100f;

    void Start()
    {
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        if(armor > 0)
        {
             armor -= amount;

        }
        else if(armor <= 0)
        {
            health -= amount;
        }
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        health = startHealth;
        armor = 0;
        gameObject.transform.position = spawnPoint.transform.position;
        Lives--;
    }

    void GameOver()
    {
        if(Lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
