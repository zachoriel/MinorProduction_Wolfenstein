using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [Header("Script Setup")]
    public PlayerStats playerStats;
    public BasicAI aiMovement;
    public LineOfSight sight;
    public bool inMenu;

    [Header("Component Setup")]
    public Image healthBar;
    public GameObject spawnPoint;
    public Animator animator;
    public NavMeshAgent agent;

    [Header("Stats")]
    public float health;
    public float startHealth = 100f;

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
            animator.SetBool("isKilled", true);
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

        if (inMenu == true)
        {
            gameObject.transform.position = spawnPoint.transform.position;
            gameObject.transform.rotation = spawnPoint.transform.rotation;

            health = startHealth;
            healthBar.fillAmount = health / startHealth;
        }
        else
        {
            aiMovement.isAlive = false;
            agent.enabled = false;
            sight.enabled = false;
        }
    }
}
