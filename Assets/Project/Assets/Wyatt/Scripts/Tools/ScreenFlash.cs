using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlash : MonoBehaviour
{

    public PlayerStats playerstats;


    void Start()
    {
        playerstats = Object.FindObjectOfType<PlayerStats>();
    }
    void Update ()
    {
        
        if(playerstats.TakingDamage)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}
