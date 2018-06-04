using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Setup")]
    public Camera fpsCam;
    public PlayerStats player;
    public WeaponSwitch weapons;
    public Animator animator;
    public Text ammoText;

    [Header("General")]
    public float range = 15f;
    public float reloadTime = 3f;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    [Header("Laser")]
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpact;
    public Light laserImpactLight;
    public int damageOverTime = 10;

    [Header("Bullets")]
    public float damage = 10f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [Header("Audio")]
    public AudioSource reloadGun;
    public AudioSource gunFire;
    public AudioSource laserBeam;
    public AudioSource emptyClip;

	// Use this for initialization
	void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player.currentGunAmmo = player.maxGunAmmo;
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

        if (player.currentGunAmmo <= 0 && player.totalAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        //if (Input.GetKeyDown(KeyCode.R) && player.currentGunAmmo < player.maxGunAmmo && player.totalAmmo > 0)
        //{
        //    StartCoroutine(Reload());
        //    return;
        //}

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && player.currentGunAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && player.currentGunAmmo == 0 && player.totalAmmo == 0)
        {
            emptyClip.Play();
        }

        if (Input.GetButton("Fire2"))
        {
            Laser();
        }
        else
        {
            if (lineRenderer.enabled)
            {
                laserBeam.Stop();
                lineRenderer.enabled = false;
                laserImpact.Stop();
                laserImpactLight.enabled = false;
                
            }
        }
	}

    IEnumerator Reload()
    {
        isReloading = true;
        lineRenderer.enabled = false;

        laserBeam.Stop();
        reloadGun.Play();

        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        player.currentGunAmmo = player.maxGunAmmo;
        player.totalAmmo -= player.maxGunAmmo;

        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunFire.Play();

        player.currentGunAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            EnemyStats enemyTarget = hit.transform.GetComponent<EnemyStats>();
            WallBreak wallTarget = hit.transform.GetComponent<WallBreak>();
            if (enemyTarget != null)
            {
                enemyTarget.TakeDamage(damage);
            }
            if (wallTarget != null)
            {
                wallTarget.TakeDamage(damage);
            }

            // Knockback (not currently used in any meaningful way)
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * -impactForce);
            }

            GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObject, 2f);
        }
    }

   

    void Laser()
    {
        Vector3 lazerStart;
        Vector3 lazerEnd;
        //player.currentAmmo--;
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

            if (enemyTarget != null)
            {
                enemyTarget.TakeDamage(damageOverTime * Time.deltaTime);
            }
            if (wallTarget != null)
            {
                wallTarget.TakeDamage(damageOverTime * Time.deltaTime);
            }

            if (!lineRenderer.enabled)
            {
                laserBeam.Play();
                lineRenderer.enabled = true;
                laserImpact.Play();
                laserImpactLight.enabled = true;
            }

            Vector3 dir = firePoint.position - target.position;
            laserImpact.transform.position = target.position + dir.normalized;
            laserImpact.transform.rotation = Quaternion.LookRotation(dir);
        }

        lineRenderer.SetPosition(0, lazerStart);
        lineRenderer.SetPosition(1, lazerEnd);
    }
}
