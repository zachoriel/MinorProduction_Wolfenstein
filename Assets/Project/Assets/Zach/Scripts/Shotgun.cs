using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shotgun : MonoBehaviour
{
    [Header("Setup")]
    public CameraScript mainCamera; // For controls
    public ChangeVolume volumeSetting;
    public AimAndControlsSetting aim;
    public Camera fpsCam;  // For shooting
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
    public float damage = 50f;
    public float fireRate;
    public float impactForce = 20f;
    public int maxGunAmmoSG = 9;
    public int currentGunAmmoSG;
    public int totalAmmoSG = 27;
    private int amountNeeded;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject breakEffect;

    [Header("Audio")]
    public AudioSource reloadGun;
    public AudioSource gunFire;
    public AudioSource emptyClip;

    // Use this for initialization
    void Start()
    {
        //ServiceLocator.instance.toggleOptions = gameObject;

        currentGunAmmoSG = maxGunAmmoSG;
        ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;
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

        if (currentGunAmmoSG <= 0 && totalAmmoSG > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentGunAmmoSG < maxGunAmmoSG && totalAmmoSG > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentGunAmmoSG > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentGunAmmoSG == 0 && totalAmmoSG == 0)
        {
            emptyClip.Play();
        }

        if (Input.GetButton("Fire2") && aim.aimAssist == true)
        {
            mainCamera.speedH = 1;
        }
        else
        {
            mainCamera.speedH = 5;
        }

        ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;
        amountNeeded = maxGunAmmoSG - currentGunAmmoSG;
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

        if (amountNeeded <= totalAmmoSG)
        {
            currentGunAmmoSG = maxGunAmmoSG;
            totalAmmoSG -= amountNeeded;
        }
        else if (amountNeeded > totalAmmoSG)
        {
            currentGunAmmoSG += totalAmmoSG;
            totalAmmoSG = 0;
        }

        //ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;

        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunFire.Play();

        currentGunAmmoSG -= 3;
        ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;

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
                if (wallTarget.health <= 0)
                {
                    GameObject explosion = Instantiate(breakEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(explosion, 6f);
                }
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
