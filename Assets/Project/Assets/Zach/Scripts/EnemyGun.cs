using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public LineOfSight lineofSight;
    public PlayerStats playerStats;

    public Transform player, firePoint;
    public GameObject enemyObject;
    public LineRenderer lineRenderer;
    public AudioSource laserBeam;

    public bool useLaser = true;

    public float range = 15f;
    public int damageOverTime = 30;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (lineofSight.CanSeeTarget == false)
        {
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
            enemyObject.transform.LookAt(player);

            if (useLaser)
            {
                Laser();
            }
            else
            {
                Shoot();
            } 
        }        
    }

    void Laser()
    {
        playerStats.TakeDamage(damageOverTime * Time.deltaTime);
        
        if (!lineRenderer.enabled)
        {
            laserBeam.Play();
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, player.position);

        Vector3 dir = firePoint.position - player.position;  // For impact effects if used
    }

    void Shoot()
    {
        Debug.Log("Placeholder");
    }
}