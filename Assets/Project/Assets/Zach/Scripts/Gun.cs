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


    [Header("Bullets")]
    public float damage = 30f;
    public float fireRate = 15f;
    public float impactForce = 20f;
    public int maxGunAmmoMG = 25;
    public int currentGunAmmoMG;
    public int totalAmmoMG = 100;
    private int amountNeeded;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [Header("Audio")]
    public AudioSource reloadGun;
    public AudioSource gunFire;
    public AudioSource emptyClip;

	// Use this for initialization
	void Start ()
    {
        currentGunAmmoMG = maxGunAmmoMG;
        ammoText.text = currentGunAmmoMG.ToString() + " / " + totalAmmoMG;
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

        if (currentGunAmmoMG <= 0 && totalAmmoMG > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentGunAmmoMG < maxGunAmmoMG && totalAmmoMG > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentGunAmmoMG > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentGunAmmoMG == 0 && totalAmmoMG == 0)
        {
            emptyClip.Play();
        }

        ammoText.text = currentGunAmmoMG.ToString() + " / " + totalAmmoMG;
        amountNeeded = maxGunAmmoMG - currentGunAmmoMG;
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

        if (amountNeeded <= totalAmmoMG)
        {
            currentGunAmmoMG = maxGunAmmoMG;
            totalAmmoMG -= amountNeeded;
        }
        else if (amountNeeded > totalAmmoMG)
        {
            currentGunAmmoMG += totalAmmoMG;
            totalAmmoMG = 0;
        }

        //ammoText.text = currentGunAmmoMG.ToString() + " / " + totalAmmoMG;

        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunFire.Play();

        currentGunAmmoMG--;
        ammoText.text = currentGunAmmoMG.ToString() + " / " + totalAmmoMG;

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
