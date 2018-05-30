using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [Header("Number Of Enemies Array")]
    public GameObject[] enemies;

    [Header("UI Element")]
    public Text enemiesAlive;

    //typical win condition
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
     
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemiesAlive.text = "Enemies Alive: " + enemies.Length;

        if (enemies.Length == 0)
        {
            Debug.Log(":P");            
        }
    }

    public void WinLevel()
    {
        SceneManager.LoadScene("NewTestScene"); // replace when implementing win
    }
}
