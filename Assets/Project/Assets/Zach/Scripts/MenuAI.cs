using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class MenuAI : MonoBehaviour 
    {
        public LineOfSight sight;
        public GameObject player;
        public NavMeshAgent agent;
        public EnemyMovement character;

        public enum State
        {
            PATROL,
            CHASE
        }

        public State state;
        private bool isAlive;

        // Patrolling variables
        public GameObject[] waypoints;
        public int waypointIndex = 0;
        public float patrolSpeed = 0.5f;

        // Chasing variables
        public float chaseSpeed = 1f;
        public GameObject target;

        void Start()
        {
            sight = GetComponent<LineOfSight>();
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<EnemyMovement>();
            player = GameObject.FindGameObjectWithTag("Player");    

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = State.PATROL;

            isAlive = true;

            StartCoroutine("FSM");
        }

        IEnumerator FSM()
        {
            while (isAlive)
            {
                switch (state)
                {
                    case State.PATROL:
                        Patrol();
                        break;
                    case State.CHASE:
                        Chase();
                        break;
                }
                yield return null;
            }
        }

        void Patrol()
        {
            agent.speed = patrolSpeed;
        
            if (Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) >= 2)
            {
                agent.destination = waypoints[waypointIndex].transform.position;
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) <= 2)
            {
                if (waypointIndex < waypoints.Length - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }

        void Chase()
        {
            agent.speed = chaseSpeed;
            agent.destination = target.gameObject.transform.position;
        
            character.Move(agent.desiredVelocity, false, false);
        }

        void Update()
        {

            if (sight.CanSeeTarget)
            {
                state = State.CHASE;

                target = player.gameObject;
                Chase();
            }

            else
            {
                state = State.PATROL;
                target = null;
            }
        }
    }