using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public LineOfSight sight;

    public GameObject player;

    public GameObject target;

    Rigidbody rb;

    public NavMeshAgent navAgent;

    public float speed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sight = GetComponent<LineOfSight>();
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (sight.CanSeeTarget)
        {
            target = player;

            transform.LookAt(target.transform.position);
            navAgent.destination = player.transform.position;
        }
    }
}
