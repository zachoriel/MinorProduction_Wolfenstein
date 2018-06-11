using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public LineOfSight sight;
    public GameObject player;

    public GameObject target;


    void Start()
    {
        sight = GetComponent<LineOfSight>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (sight.CanSeeTarget)
        {
            target = player.gameObject;
            transform.LookAt(target.transform.position);
        }
    }
}
