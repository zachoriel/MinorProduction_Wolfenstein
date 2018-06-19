using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [Header("Script Setup")]
    public PlayerStats playerStats;
    public BasicAI aiMovement;
    public LineOfSight sight;
    public Win winCondition;
    public bool inMenu;

    [Header("Component Setup")]
    public Image healthBar;
    public GameObject spawnPoint;
    public Animator animator;
    public NavMeshAgent agent;
    public Text enemiesAliveText;
    Rigidbody rb;

    [Header("Stats")]
    public float health;
    public float startHealth = 100f;
    private float timeDead;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        BigListOfEnemies.instance.enemyList.Add(this.gameObject);
        health = startHealth;
        playerStats = FindObjectOfType<PlayerStats>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0f && !isDead)
        {
            animator.SetBool("isKilled", true);
            Die(); 
        }
    }

    void Die()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ; // Prevents dead enemies from falling through the floor
        //rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ; // Prevents dead enemies from spinning when colliding with player
        isDead = true;
        playerStats.Score += 10;
        playerStats.scoreText.text = playerStats.Score.ToString();

        if (playerStats.Score % 500 == 0)
        {
            playerStats.Lives++;
        }

        aiMovement.isAlive = false;
        agent.enabled = false;
        sight.enabled = false;
        sight.CanSeeTarget = false;         // Disabling just the script doesn't switch off the bool, and just disabling the bool for some reason doesn't work. 
        winCondition.enemiesAlive--;
        enemiesAliveText.text = "Enemies Alive: " + winCondition.enemiesAlive.ToString();

        StartCoroutine("DeathTimer");
    }

    IEnumerator DeathTimer()
    {
        for (int i = 0; i < 30f; i++)
        {
            timeDead++;
            yield return new WaitForSeconds(1f);
        }

        if (timeDead >= 30f)
        {
            Destroy(gameObject);
        }

        yield return null;
    }
}
