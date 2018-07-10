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
    public EnemyGun gun;
    public bool inMenu;

    [Header("Component Setup")]
    public Image healthBar;
    public GameObject spawnPoint;
    public Animator animator;
    public Animator droneAnimator;
    public NavMeshAgent agent;
    public Text enemiesAliveText;
    public AudioSource deathSound;
    Rigidbody rb;

    [Header("Stats")]
    public float health;
    public float startHealth = 100f;
    private float timeDead;
    private bool isDead = false;
    int randomDeath;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (inMenu)
        {
            return;
        }
        else
        {
            BigListOfEnemies.instance.enemyList.Add(this.gameObject);
        }

        health = startHealth;
        playerStats = FindObjectOfType<PlayerStats>();

        deathSound = GetComponent<AudioSource>();

        gun = GetComponentInChildren<EnemyGun>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0f && !isDead)
        {
            if (gameObject.tag == "Enemy")
            {
                randomDeath = UnityEngine.Random.Range(1, 3);
                animator.SetBool("isKilled", true);
                animator.SetInteger("deathAnim", randomDeath);
            }
            if (gameObject.tag == "Drone")
            {
                droneAnimator.SetBool("isKilled", true);
            }
            Die(); 
        }
    }

    void Die()
    {
        deathSound.Play();
        gun.TurnOffEffects();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ; // Prevents dead enemies from falling through the floor
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
        sight.enemyGun = null;

        if (inMenu)
        {
            return;
        }
        else
        {
            winCondition.enemiesAlive--;
        }

        enemiesAliveText.text = "Enemies Alive: " + winCondition.enemiesAlive.ToString();
        gameObject.GetComponent<BoxCollider>().enabled = false;

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
