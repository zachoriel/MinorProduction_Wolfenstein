using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public LineOfSight sight;

    public GameObject player;

    public GameObject target;

    Rigidbody rb;


    public float speed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sight = GetComponent<LineOfSight>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {

        if (sight.CanSeeTarget)
        {
            target = player;

            transform.LookAt(target.transform.position);

            transform.RotateAround(player.transform.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
