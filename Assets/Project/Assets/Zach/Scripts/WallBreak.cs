using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    public float health;
    private float startHealth = 100f;

    // Use this for initialization
    void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
