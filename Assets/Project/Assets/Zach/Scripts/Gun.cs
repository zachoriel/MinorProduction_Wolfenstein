using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Menu Stuff")] 
    public CameraScript mainCamera;
    public ChangeVolume volumeSetting;
    public AimAndControlsSetting aim;
    public Text volumeText;
    public Toggle aimToggle;
    public Toggle invertedToggle;
    public bool inMenu;

    [Header("Setup")]
    public Camera fpsCam;
    public PlayerStats player;
    public WeaponSwitch weapons;
    public Animator animator;
    public Text ammoText; 

    [Header("General")]
    public float range = 15f;
    private float distanceToEnemy;
    public float reloadTime = 3f;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    [Header("Bullets")]
    public float damage = 20f;
    public float fireRate = 15f;
    public float impactForce = 20f;
    public int maxGunAmmoMG = 25;
    public int currentGunAmmoMG;
    public int totalAmmoMG = 100;
    private int amountNeeded;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject breakEffect;

    [Header("Audio")]
    public AudioSource reloadGun;
    public AudioSource gunFire;
    public AudioSource emptyClip;

	// Use this for initialization
	void Start ()
    {
        //ServiceLocator.instance.toggleOptions = gameObject;

        if (inMenu == true)
        {
            totalAmmoMG = 1000000;
        }
        else
        {
            totalAmmoMG = 100;
        }

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

        if (Input.GetButton("Fire2") && aim.aimAssist == true)
        {
            mainCamera.speedH = 1;
        }
        else
        {
            mainCamera.speedH = 5;
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
            SceneLoader buttonsTarget = hit.transform.GetComponent<SceneLoader>();

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

            if (buttonsTarget != null)
            {
                if (buttonsTarget.tag == "PlayButton")
                {
                    buttonsTarget.Play();
                }
                else if (buttonsTarget.tag == "SettingsButton")
                {
                    buttonsTarget.Settings();
                }
                else if (buttonsTarget.tag == "AimToggle")
                {
                    if (aimToggle.isOn == true)
                    {
                        aimToggle.isOn = false;
                    }
                    else
                    {
                        aimToggle.isOn = true;
                    }
                }
                else if (buttonsTarget.tag == "UpVolume")
                {
                    volumeSetting.UpVol();
                }
                else if (buttonsTarget.tag == "DownVolume")
                {
                    volumeSetting.DownVol();
                }
                else if (buttonsTarget.tag == "InvertedToggle")
                {
                    if (invertedToggle.isOn == false)
                    {
                        invertedToggle.isOn = true;
                    }
                    else
                    {
                        invertedToggle.isOn = false;
                    }
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
