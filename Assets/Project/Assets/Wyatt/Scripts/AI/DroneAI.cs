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

    public Animator animator;

    public float speed;

    public bool isAlive;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sight = GetComponent<LineOfSight>();
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        isAlive = true;
    }
    void Update()
    {
        if (isAlive)
        {
            if (sight.CanSeeTarget)
            {
                animator.SetBool("firing", true);
                target = player;

                transform.LookAt(target.transform.position);
                navAgent.destination = player.transform.position;
            }
            else
            {
                animator.SetBool("firing", false);
            }
        }
        else
        {
            target = null;
        }
    }
}
