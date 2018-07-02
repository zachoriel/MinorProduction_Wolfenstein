﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGun : MonoBehaviour
{
    [Header("Script Setup")]
    public LineOfSight lineofSight; 
    public PlayerStats playerStats;
    public PlayerMotor playerMovement;
    public DroneStats drone;

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
        playerMovement = FindObjectOfType<PlayerMotor>();
        drone = FindObjectOfType<DroneStats>();
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
        else if (lineofSight.CanSeeTarget == true)
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

        if (drone.isDead)
        {
            laserBeam.Stop();
        }
    }

    void Laser()
    {
        if (isDrone && playerStats.TakingDamage)
        {
            if (playerStats.godModeHiCacie == false)
            {
                playerStats.TakeDamage(damageOverTime * Time.deltaTime);
            }
            else
            {
                return;
            }
        }
        else
        {
            if (playerStats.godModeHiCacie == false)
            {
                playerMovement.Slow();
            }
            else
            {
                return;
            }
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