using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
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

        if (enemies.Length == 0)
        {
            Debug.Log(":P");            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "WinItem" && enemiesAlive == 0)
        {
            WinLevel();
        }
    }

    public void WinLevel()
    {
        SceneManager.LoadScene("ShipLevel"); // replace when implementing win
    }
}
