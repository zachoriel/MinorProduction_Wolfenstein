using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public LineOfSight lineofSight;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public int maxAmmo = 10;
    private int currentAmo;
    public float reloadTime = 3f;
    public bool isReloading = false;

    public float nextTimeToFire = 0f;

    Transform player;
    public Transform firePoint;
    public GameObject bullet, enemyObject;

    void Awake()
    {
        currentAmo = maxAmmo;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (lineofSight.CanSeeTarget == true)
        {
            enemyObject.transform.LookAt(player);
            Shooting();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime - 0.25f);

        //yield return new WaitForSeconds(0.25f);

        currentAmo = maxAmmo;
        isReloading = false;
    }

    void Shooting()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        
    }
}