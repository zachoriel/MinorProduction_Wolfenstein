using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallBreak : MonoBehaviour
{
    [Header("Stats")]
    public float health;

    public Animator animator;
    public PlayerStats player;
    public GameObject achievement;

    // Use this for initialization
    void Start()
    {
        //achievement.SetActive(false);
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
        if (gameObject.tag == "AchievementWall")
        {
            achievement.SetActive(true);
            animator.SetTrigger("AchievementEarned");
            player.Score += 25;
            player.scoreText.text = player.Score.ToString();
        }

        Destroy(gameObject);
    }
}
