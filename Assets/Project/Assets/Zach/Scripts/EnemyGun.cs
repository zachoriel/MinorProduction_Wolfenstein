using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [Header("Script Setup")]
    public LineOfSight lineofSight;
    public PlayerStats playerStats;
    public Movement playerMovement;
    public BasicAI ai;

    [Header("Component Setup")]
    public Transform player;
    public Transform firePoint;
    public GameObject enemyObject;
    public LineRenderer lineRenderer;
    public AudioSource laserBeam;
    public Animator animator;

    [Header("Type Of Enemy")]
    public bool useLaser = true;
    public bool isDrone;

    [Header("Stats")]
    public float range = 15f;
    public int damageOverTime = 30;

    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        player = GameObject.FindWithTag("Player").transform;
        playerMovement = FindObjectOfType<Movement>();
        ai = FindObjectOfType<BasicAI>();
    }

    void Update()
    {
        if (lineofSight.CanSeeTarget == false)
        {
            animator.SetBool("isFiringLaser", false);

            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    laserBeam.Stop();
                    lineRenderer.enabled = false;
                }
            }
            //return;
        }
        else if (lineofSight.CanSeeTarget == true && ai.state != BasicAI.State.FLEE)
        {
            animator.SetBool("isFiringLaser", true);

            if (useLaser)
            {
                Laser();
            }
            else
            {
                Shoot();
            } 
        }  
        else if (lineofSight.CanSeeTarget == true && ai.state == BasicAI.State.FLEE)
        {
            animator.SetBool("isFiringLaser", false);

            if (useLaser)
            {

                if (lineRenderer.enabled)
                {
                    laserBeam.Stop();
                    lineRenderer.enabled = false;
                }
            }
        }
    }

    void Laser()
    {
        if (isDrone && playerStats.TakingDamage)
        {
            playerStats.TakeDamage(damageOverTime * Time.deltaTime);
        }
        else
        {
            playerMovement.Slow();
        }
        
        if (!lineRenderer.enabled)
        {
            laserBeam.Play();
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, player.position);

        //Vector3 dir = firePoint.position - player.position;  // For impact effects if used
    }

    void Shoot()
    {
        Debug.Log("Placeholder");
    }
}