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
    public Text batteryText;
    public Light flashlight;

    [Header("General")]
    public float range;
    public float reloadTime = 3f;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    [Header("Bullets")]
    public float damage = 50f;
    public float fireRate;
    public float impactForce = 20f;
    public int maxGunAmmoSG = 3;
    public int currentGunAmmoSG;
    public int totalAmmoSG = 27;
    public float batteryLife = 100f;
    private int wholeBatteryLife;
    private int amountNeeded;
    public int AmountOfProjectiles;
    public Vector3 spread;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject breakEffect;

    [Header("Audio")]
    public AudioSource reloadGun;
    public AudioSource gunFire;
    public AudioSource emptyClip;
    public AudioSource lightOn;
    public AudioSource lightOff;

    // Use this for initialization
    void Start()
    {
        volumeSetting = FindObjectOfType<ChangeVolume>();
        aim = FindObjectOfType<AimAndControlsSetting>();

        currentGunAmmoSG = maxGunAmmoSG;
        ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;
        batteryText.text = batteryLife.ToString() + "%";
    }

    //void OnEnable()
    //{
    //    isReloading = false;
    //    animator.SetBool("Reloading", false);
    //}

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

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentGunAmmoSG > 0)
        {
            animator.SetBool("isFiring", true);
            nextTimeToFire = Time.time + 1f / fireRate;
            for (int i = 0; i < AmountOfProjectiles; i++)
            {
                Shoot();
            }
        }
        else if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentGunAmmoSG == 0 && totalAmmoSG == 0)
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

        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isFiring", false);
        }

        if (Input.GetKeyDown(KeyCode.F) && batteryLife > 0)
        {
            if (flashlight.enabled == true)
            {
                lightOff.Play();
                flashlight.enabled = false;
            }
            else
            {
                lightOn.Play();
                flashlight.enabled = true;
            }
        }

        if (flashlight.enabled == true && batteryLife <= 0)
        {
            flashlight.enabled = false;
        }

        if (flashlight.enabled)
        {
            batteryLife -= 0.85f * Time.deltaTime;
        }

        ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;
        amountNeeded = maxGunAmmoSG - currentGunAmmoSG;

        wholeBatteryLife = Mathf.RoundToInt(batteryLife);
        Mathf.Clamp(wholeBatteryLife, 0, 100);
        batteryText.text = "Light Battery: " + wholeBatteryLife.ToString() + "%";
    }

    IEnumerator Reload()
    {
        isReloading = true;
        weapons.enabled = false;

        animator.SetBool("Reloading", true);
        animator.SetBool("isFiring", false);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        reloadGun.Play();

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
        weapons.enabled = true;
    }

    void Shoot() 
    {
        Vector3 direction = fpsCam.transform.forward;

        spread += fpsCam.transform.up * Random.Range(-1f, 1f);
        spread += fpsCam.transform.right * Random.Range(-1f, 1f);
        direction += spread.normalized * Random.Range(0f, 0.2f);
        muzzleFlash.Play();
        gunFire.Play();

        currentGunAmmoSG--;
        ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            EnemyStats enemyTarget = hit.transform.GetComponent<EnemyStats>();
            WallBreak wallTarget = hit.transform.GetComponent<WallBreak>();
            DroneStats drone = hit.transform.GetComponent<DroneStats>();
            DamagedEnemy firstEnemy = hit.transform.GetComponent<DamagedEnemy>();

            if (firstEnemy != null)
            {
                firstEnemy.TakeDamage(damage);
            }

            if (drone != null)
            {
                drone.TakeDamage(damage);
            }

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

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * -impactForce);
            }

            GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObject, 2f);
        }
    }
}


//{
//    [Header("Setup")]
//    public CameraScript mainCamera; // For controls
//    public ChangeVolume volumeSetting;
//    public AimAndControlsSetting aim;
//    public Camera fpsCam;  // For shooting
//    public PlayerStats player;
//    public WeaponSwitch weapons;
//    public Animator animator;
//    public Text ammoText;
//    public Light flashlight;

//    [Header("General")]
//    public float range;
//    public float reloadTime = 3f;
//    private bool isReloading = false;
//    private float nextTimeToFire = 0f;

//    [Header("Bullets")]
//    public float damage = 50f;
//    public float fireRate;
//    public float impactForce = 20f;
//    public int maxGunAmmoSG = 9;
//    public int currentGunAmmoSG;
//    public int totalAmmoSG = 27;
//    private int amountNeeded;
//    public ParticleSystem muzzleFlash;
//    public GameObject impactEffect;
//    public GameObject breakEffect;

//    [Header("Audio")]
//    public AudioSource reloadGun;
//    public AudioSource gunFire;
//    public AudioSource emptyClip;

//    // Use this for initialization
//    void Start()
//    {
//        volumeSetting = FindObjectOfType<ChangeVolume>();
//        aim = FindObjectOfType<AimAndControlsSetting>();

//        currentGunAmmoSG = maxGunAmmoSG;
//        ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;
//    }

//    void OnEnable()
//    {
//        isReloading = false;
//        animator.SetBool("Reloading", false);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isReloading)
//        {
//            return;
//        }

//        if (currentGunAmmoSG <= 0 && totalAmmoSG > 0)
//        {
//            StartCoroutine(Reload());
//            return;
//        }

//        if (Input.GetKeyDown(KeyCode.R) && currentGunAmmoSG < maxGunAmmoSG && totalAmmoSG > 0)
//        {
//            StartCoroutine(Reload());
//            return;
//        }

//        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentGunAmmoSG > 0)
//        {
//            animator.SetBool("isFiring", true);
//            nextTimeToFire = Time.time + 1f / fireRate;
//            Shoot();
//        }
//        else if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentGunAmmoSG == 0 && totalAmmoSG == 0)
//        {
//            emptyClip.Play();
//        }

//        if (Input.GetKey(KeyCode.LeftShift) && aim.aimAssist == true)
//        {
//            mainCamera.speedH = 1;
//        }
//        else
//        {
//            mainCamera.speedH = 5;
//        }

//        if (Input.GetButtonUp("Fire1"))
//        {
//            animator.SetBool("isFiring", false);
//        }

//        if (Input.GetButtonDown("Fire2"))
//        {
//            if (flashlight.enabled == true)
//            {
//                flashlight.enabled = false;
//            }
//            else
//            {
//                flashlight.enabled = true;
//            }
//        }

//        ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;
//        amountNeeded = maxGunAmmoSG - currentGunAmmoSG;
//    }

//    IEnumerator Reload()
//    {
//        isReloading = true;

//        reloadGun.Play();

//        animator.SetBool("Reloading", true);

//        yield return new WaitForSeconds(reloadTime - 0.25f);

//        animator.SetBool("Reloading", false);
//        yield return new WaitForSeconds(0.25f);

//        if (amountNeeded <= totalAmmoSG)
//        {
//            currentGunAmmoSG = maxGunAmmoSG;
//            totalAmmoSG -= amountNeeded;
//        }
//        else if (amountNeeded > totalAmmoSG)
//        {
//            currentGunAmmoSG += totalAmmoSG;
//            totalAmmoSG = 0;
//        }

//        //ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;

//        isReloading = false;
//    }

//    void Shoot()
//    {
//        muzzleFlash.Play();
//        gunFire.Play();

//        currentGunAmmoSG -= 3;
//        ammoText.text = currentGunAmmoSG.ToString() + " / " + totalAmmoSG;

//        RaycastHit hit;
//        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
//        {
//            EnemyStats enemyTarget = hit.transform.GetComponent<EnemyStats>();
//            WallBreak wallTarget = hit.transform.GetComponent<WallBreak>();
//            DroneStats drone = hit.transform.GetComponent<DroneStats>();
//            DamagedEnemy firstEnemy = hit.transform.GetComponent<DamagedEnemy>();

//            if (firstEnemy != null)
//            {
//                firstEnemy.TakeDamage(damage);
//            } 

//            if (drone != null)
//            {
//                drone.TakeDamage(damage);
//            }

//            if (enemyTarget != null)
//            {
//                enemyTarget.TakeDamage(damage);
//            }

//            if (wallTarget != null)
//            {
//                wallTarget.TakeDamage(damage);
//                if (wallTarget.health <= 0)
//                {
//                    GameObject explosion = Instantiate(breakEffect, hit.point, Quaternion.LookRotation(hit.normal));
//                    Destroy(explosion, 6f);
//                }
//            }

//            if (hit.rigidbody != null)
//            {
//                hit.rigidbody.AddForce(hit.normal * -impactForce);
//            }

//            GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
//            Destroy(impactObject, 2f);
//        }
//    }
//}
