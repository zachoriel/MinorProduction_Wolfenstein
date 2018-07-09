using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DroneStats : MonoBehaviour
{
    [Header("Script Setup")]
    public PlayerStats playerStats;
    public DroneAI aiMovement;
    public LineOfSight sight;
    public Win winCondition;
    public DroneShootScript droneShoot;
    public bool inMenu;

    [Header("Component Setup")]
    public Image healthBar;
    //public GameObject spawnPoint;
    public Animator animator;
    //public Animator droneAnimator;
    public NavMeshAgent agent;
    public AudioSource droneDeath;
    //public AudioSource laserBolt;
    //public Text enemiesAliveText;
    Rigidbody rb;
    Collider collider;

    [Header("Stats")]
    public float health;
    public float startHealth = 100f;
    private float timeDead;

    [HideInInspector]
    public bool isDead = false;

    void Awake()
    {
        health = startHealth;
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        sight = GetComponent<LineOfSight>();
        winCondition = FindObjectOfType<Win>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        aiMovement = GetComponent<DroneAI>();
        droneShoot = FindObjectOfType<DroneShootScript>();
        collider = GetComponent<Collider>();
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
        droneDeath.Play();
        //laserBolt.Stop();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = true;
        rb.isKinematic = true;
        isDead = true;
        playerStats.Score += 10;
        playerStats.scoreText.text = playerStats.Score.ToString();

        if (playerStats.Score % 500 == 0)
        {
            playerStats.Lives++;
        }

        aiMovement.isAlive = false;
        agent.enabled = false;
        sight.CanSeeTarget = false;         // Disabling just the script doesn't switch off the bool, and just disabling the bool for some reason doesn't work. 
        sight.enabled = false;
        droneShoot.useLaser = false;
        droneShoot.lineRenderer.enabled = false;
        collider.enabled = false;
        //winCondition.enemiesAlive--;
        //enemiesAliveText.text = "Enemies Alive: " + winCondition.enemiesAlive.ToString();
        //gameObject.GetComponent<BoxCollider>().enabled = false;

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
