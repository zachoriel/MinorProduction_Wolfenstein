using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public ControlPanel panel;
    public PlayerStats player;
    public SceneFader fader;

    [Header("Number Of Enemies Array")]
    public GameObject[] enemies;
    public int enemiesAlive;

    [Header("UI")]
    public Text enemiesAliveText;
    public Text objectiveText;

    //typical win condition
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesAlive = enemies.Length;
        enemiesAliveText.text = "Enemies Alive: " + enemies.Length.ToString();
        objectiveText.text = "Objective: Reach the escape pods!";
    }
     
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (panel.enteredKeyCode)
            {
                if (enemiesAlive <= 15 && enemiesAlive > 10)
                {
                    player.Score += player.Score * 0; // No score multiplier
                }

                if (enemiesAlive <= 10 && enemiesAlive > 5)
                {
                    player.Score += player.Score * 1; // x2 score multiplier
                }

                if (enemiesAlive <= 5 && enemiesAlive > 0)
                {
                    player.Score += player.Score * 1.5f; // x2.5 score multiplier
                }

                if (enemiesAlive == 0)
                {
                    player.Score += player.Score * 2; // x3 score multiplier
                }

                WinLevel();
            }
            else
            {
                Debug.LogError("Warning: Cheater alert. Self-destruct sequence started. Application closing in 3...2...1...");
                Application.Quit();
            }
        }
    }

    public void WinLevel()
    {
        fader.FadeTo("WinScene");
        //SceneManager.LoadScene("WinScene");
    }
}
