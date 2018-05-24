using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Setup")]
    public PlayerStats player;
    public WeaponSwitch weapons;

    [Header("Laser")]
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public AudioSource laserSound;
    public int damageOverTime = 10;

    [Header("Bullets")]
    public float damage = 10f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    [Header("General")]
    public float range = 15f;
    public float reloadTime = 3f;
    public bool isReloading = false;

    public Camera fpsCam;
    //public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource reloadSound, fireSound;

    public float nextTimeToFire = 0f;

    public Animator animator;

    public Text ammoText;

	// Use this for initialization
	void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player.currentAmmo = player.maxAmmo;
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

        if (player.currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && player.currentAmmo < player.maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && player.currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        if (Input.GetButton("Fire2") && player.currentAmmo > 0)
        {
            Laser();
        }
        else
        {
            if (lineRenderer.enabled)
            {
                laserSound.Stop();
                lineRenderer.enabled = false;
            }
        }
	}

    IEnumerator Reload()
    {
        isReloading = true;
        lineRenderer.enabled = false;

        laserSound.Stop();
        reloadSound.Play();

        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        player.currentAmmo = player.maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        //muzzleFlash.Play();
        fireSound.Play();

        player.currentAmmo--;

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
        lazerEnd = transform.position + transform.forward * 20000;
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
                laserSound.Play();
                lineRenderer.enabled = true;
            }

            Vector3 dir = firePoint.position - hit.transform.position;
            impactEffect.transform.position = target.position + dir.normalized;
            impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        }

        lineRenderer.SetPosition(0, lazerStart);
        lineRenderer.SetPosition(1, lazerEnd);
    }
}
