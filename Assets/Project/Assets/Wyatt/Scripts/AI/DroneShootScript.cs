using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShootScript : MonoBehaviour
{
    [Header("Script Setup")]
    public LineOfSight lineofSight;
    public PlayerStats playerStats;

    [Header("Component Setup")]
    public Transform player;
    public Transform firePoint;
    public GameObject enemyObject;
    public LineRenderer lineRenderer;
    public AudioSource laserBeam;

    [Header("ConeVariables")]    
    public float scaleLimit;
    public float z;

    [Header("Timer")]
    public float startTime;
    public float timer;

    [Header("Type Of Enemy")]
    public bool useLaser = true;

    [Header("Stats")]
    public float range = 15f;
    public int Damage = 30;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        timer = startTime;
    }

    void Update()
    {
        if (lineofSight.CanSeeTarget == false)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    //laserBeam.Stop();
                    lineRenderer.enabled = false;
                }
            }
            //return;
        }
        else if (lineofSight.CanSeeTarget == true)
        {
            //enemyObject.transform.rotation = new Quaternion(0,0,0,0);
            //enemyObject.transform.LookAt(player);                             // NO LONGER NEEDED CAUSE NAVMESH DOES IT


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
        timer-= Time.deltaTime;

        if(timer <= 0)
        {
            Vector3 direction = Random.insideUnitCircle * scaleLimit;
            direction.z = z; // circle is at Z units 
            direction = transform.TransformDirection(direction.normalized);
            //Raycast and debug
            Ray r = new Ray(transform.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                if(hit.collider.tag == "Player")
                {
                    playerStats.TakeDamage(Damage);
                }
                Debug.DrawLine(transform.position, hit.point);
            }


            if (!lineRenderer.enabled)
            {
                //laserBeam.Play();
                lineRenderer.enabled = true;
            }

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);
            //lineRenderer.enabled = false;
            timer = startTime;

        }
        

    }
    //playerStats.TakeDamage(damageOverTime * Time.deltaTime);

    //Vector3 dir = firePoint.position - player.position;  // For impact effects if used

    void Shoot()
    {
        Debug.Log("Placeholder");
    }

    //IEnumerable Timer()
    //{
    //    yield return new WaitForSeconds(1f);
    //}
}
