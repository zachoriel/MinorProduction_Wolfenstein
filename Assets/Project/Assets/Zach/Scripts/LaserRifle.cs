﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LaserRifle : MonoBehaviour
{ 
    [Header("Setup")]
    public CameraScript mainCamera; // For controls
    public ChangeVolume volumeSetting;
    public AimAndControlsSetting aim;
    public Camera fpsCam; // For shooting
    public PlayerStats player;
    public WeaponSwitch weapons;
    public Animator animator;
    public Text ammoText;
    public Light flashlight;

    [Header("General")]
    public float range = 15f;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    [Header("Laser")]
    public Transform firePoint;
    public LineRenderer lineRenderer;
    //public ParticleSystem laserImpact;
    //public Light laserImpactLight;
    public int damageOverTime = 20;
    public float energy;
    public float maxEnergy = 100f;
    private int wholeEnergy;
    public GameObject breakEffect;

    [Header("Audio")]
    public AudioSource laserBeam;
    public AudioSource rechargeGun;

	// Use this for initialization
	void Start ()
    {
        //ServiceLocator.instance.toggleOptions = gameObject;

        lineRenderer = GetComponent<LineRenderer>();
        volumeSetting = FindObjectOfType<ChangeVolume>();
        aim = FindObjectOfType<AimAndControlsSetting>();
        energy = maxEnergy;
        ammoText.text = energy.ToString() + "%";
        lineRenderer.enabled = false;
	}

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update ()
    {
        if (isReloading)
        {
            return;
        }

        if (energy < 0.5f && energy >= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && energy < maxEnergy)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("isFiring", true);
            Laser();
        }
        else
        {
            if (lineRenderer.enabled)
            {
                laserBeam.Stop();
                lineRenderer.enabled = false;
                //laserImpact.Stop();
                //laserImpactLight.enabled = false;
                
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isFiring", false);
        }

        if (Input.GetKey(KeyCode.LeftShift) && aim.aimAssist == true)
        {
            mainCamera.speedH = 1;
        }
        else
        {
            mainCamera.speedH = 5;
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            if (flashlight.enabled == true)
            {
                flashlight.enabled = false;
            }
            else
            {
                flashlight.enabled = true;
            }
        }

        wholeEnergy = Mathf.RoundToInt(energy);
        ammoText.text = wholeEnergy.ToString() + "%";
    }

    IEnumerator Reload()
    {
        laserBeam.Stop();
        lineRenderer.enabled = false;

        isReloading = true;

        rechargeGun.Play();

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        energy = maxEnergy;
        //ammoText.text = energy.ToString();

        isReloading = false;
    }

    void Laser()
    {
        energy -= 10 * Time.deltaTime;

        Vector3 lazerStart;
        Vector3 lazerEnd;
        lazerStart = firePoint.position;
        lazerEnd = transform.position + transform.forward * 50000;
        lineRenderer.SetPosition(0, lazerStart);

        RaycastHit hit;
        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, range))
        {
            lazerEnd = hit.point;
            EnemyStats enemyTarget = hit.transform.GetComponent<EnemyStats>();
            WallBreak wallTarget = hit.transform.GetComponent<WallBreak>();
            Transform target = hit.transform;
            DroneStats drone = hit.transform.GetComponent<DroneStats>();
            DamagedEnemy firstEnemy = hit.transform.GetComponent<DamagedEnemy>();

            if (firstEnemy != null)
            {
                firstEnemy.TakeDamage(damageOverTime * Time.deltaTime);
            }

            if (drone != null)
            {
                drone.TakeDamage(damageOverTime * Time.deltaTime);
            }

            if (enemyTarget != null)
            {
                enemyTarget.TakeDamage(damageOverTime * Time.deltaTime);
            }

            if (wallTarget != null)
            {
                wallTarget.TakeDamage(damageOverTime * Time.deltaTime);
                if (wallTarget.health <= 0)
                {
                    GameObject explosion = Instantiate(breakEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(explosion, 6f);
                }
            }

            if (!lineRenderer.enabled)
            {
                laserBeam.Play();
                lineRenderer.enabled = true;
                //laserImpact.Play();
                //laserImpactLight.enabled = true;
            }

            Vector3 dir = firePoint.position - target.position;
            //laserImpact.transform.position = target.position + dir.normalized;
            //laserImpact.transform.rotation = Quaternion.LookRotation(dir);
        }

        lineRenderer.SetPosition(0, lazerStart);
        lineRenderer.SetPosition(1, lazerEnd);
    }
}
