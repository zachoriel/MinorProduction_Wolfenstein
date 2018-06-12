using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [Header("Number Of Enemies Array")]
    public GameObject[] enemies;

    //typical win condition
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
        if (other.tag == "Player" && gameObject.tag == "WinItem" && enemies.Length <= 0)
        {
            WinLevel();
        }
    }

    public void WinLevel()
    {
        SceneManager.LoadScene("ShipLevel"); // replace when implementing win
    }
}
