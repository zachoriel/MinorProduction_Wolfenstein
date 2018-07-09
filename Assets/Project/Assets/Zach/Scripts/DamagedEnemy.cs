using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagedEnemy : MonoBehaviour
{
    [Header("Component Setup")]
    public Animator animator;
    public Image healthBar;
    public AudioSource death;
    Rigidbody rb;

    [Header("Stats")]
    public float health;
    private float startHealth = 100f;
    private float timeDead;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        health = startHealth / 5;
        healthBar.fillAmount = health / startHealth;
        death = GetComponent<AudioSource>();
	}

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0f)
        {
            animator.SetBool("isKilled", true);
            Die();
        }
    }

    void Die()
    {
        death.Play();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        gameObject.GetComponent<BoxCollider>().enabled = false;

        StartCoroutine("DeathTimer");
    }

    IEnumerator DeathTimer()
    {
        for (int i = 0; i < 30f; i++)
        {
            timeDead++;
            yield return new WaitForSeconds(1f);
        }

        if (timeDead >= 30f)
        {
            Destroy(gameObject);
        }

        yield return null;
    }
}
