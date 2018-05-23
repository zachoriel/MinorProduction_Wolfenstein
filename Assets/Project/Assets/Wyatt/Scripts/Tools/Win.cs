using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Win : MonoBehaviour
{
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
}
