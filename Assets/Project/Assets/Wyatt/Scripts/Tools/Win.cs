using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public GameObject[] enemies;
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
}
