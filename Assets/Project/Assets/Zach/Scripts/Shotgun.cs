using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shotgun : MonoBehaviour
{
    [Header("Setup")]
    public Camera fpsCam;
    public PlayerStats player;
    public WeaponSwitch weapons;
    public Animator animator;
    public Text ammoText;

    [Header("General")]
    public float range;
    public float reloadTime = 3f;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    [Header("Bullets")]
    public float damage = 30f;
    public float fireRate;
    public float impactForce = 20f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [Header("Audio")]
    public AudioSource reloadGun;
    public AudioSource gunFire;
    public AudioSource emptyClip;

    // Use this for initialization
    void Start()
    {
        player.currentGunAmmo = player.maxGunAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
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

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && player.currentGunAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && player.currentGunAmmo == 0 && player.totalAmmo == 0)
        {
            emptyClip.Play();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

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
}
