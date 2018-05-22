using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Win : MonoBehaviour
{
    GameObject[] enemies;


    //boss idea




    //typical win condition
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {

        if(enemies.Length == 0)
        {
            
            
        }
    }




}
