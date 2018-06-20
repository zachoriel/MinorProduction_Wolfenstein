using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlash : MonoBehaviour
{

    public PlayerStats playerstats;


    void Start()
    {
        //playerstats = Object.FindObjectOfType<PlayerStats>();
    }
    void Update ()
    {
        
        if(playerstats.TakingDamage)
        {
            GameObject.Find("ScreenFlash").SetActive(true);
        }
        else
        {
            GameObject.Find("ScreenFlash").SetActive(false);
        }
    }
}
