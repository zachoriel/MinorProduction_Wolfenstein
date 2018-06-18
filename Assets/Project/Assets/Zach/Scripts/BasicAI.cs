using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class BasicAI : MonoBehaviour
{
    public LineOfSight sight;
    public GameObject player;
    public NavMeshAgent agent;
    public EnemyMovement character;

    public GameObject DronePrefab;
    public GameObject instantiated;
        public enum State
        {
            PATROL,
            CHASE
        }

        public State state;
        public bool isAlive;

        [Header ("Patrolling variables")]
        public GameObject[] waypoints;
        public int waypointIndex = 0;
        public float patrolSpeed = 0.5f;

        [Header ("Chasing variables")]
        public float chaseSpeed = 1f;
        public GameObject target;
        public Transform me;

        public int instantiateValue;
        public bool canInstantiate;
        

        void Start()
        {
            sight = GetComponent<LineOfSight>();
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<EnemyMovement>();
            player = GameObject.FindGameObjectWithTag("Player");
            me = gameObject.transform;
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

        if (instantiateValue < 3)
        {
            instantiated = Instantiate(DronePrefab, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y, transform.position.z),
                Quaternion.identity, gameObject.transform);
            instantiateValue++;
        }
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