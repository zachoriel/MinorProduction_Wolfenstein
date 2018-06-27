using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigListOfEnemies : MonoBehaviour {


    public List<GameObject> enemyList;
    public static BigListOfEnemies instance;
    void Awake()
    {
        enemyList = new List<GameObject>();
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(this); }
    }

}
