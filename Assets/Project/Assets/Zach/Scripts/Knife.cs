using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Knife : MonoBehaviour
{
    [Header("Setup")]
    public PlayerStats player;
    public WeaponSwitch weapons;
    public Animator animator;
    public Animation knifeSwing;
    public Text ammoText;

    [Header("General")]
    public int damage = 10;

    [Header("Audio")]
    public AudioSource knifeAttack;
    public AudioSource knifeHit;

	// Use this for initialization
	void Start ()
    {
        player.currentGunAmmo = player.maxGunAmmo;
	}

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            Swing();
        }
	}

    void Swing()
    {
        knifeAttack.Play();
        knifeSwing.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyStats enemyTarget = other.transform.GetComponent<EnemyStats>();
        WallBreak wallTarget = other.transform.GetComponent<WallBreak>();

        knifeHit.Play();

        if (enemyTarget != null)
        {
            enemyTarget.TakeDamage(damage);
        }
        if (wallTarget != null)
        {
            wallTarget.TakeDamage(damage);
        }
    }
}
