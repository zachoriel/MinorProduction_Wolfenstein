﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class PreMergeBasicAI : MonoBehaviour 
    {
        public LineOfSight sight;
        public GameObject player;
        public NavMeshAgent agent;
        public EnemyMovement character;
        public EnemyStats stats;

        public enum State
        {
            PATROL,
            CHASE,
            FLEE
        }

        public State state;
        public bool isAlive;

        // Patrolling variables
        public GameObject[] waypoints;
        public int waypointIndex = 0;
        public float patrolSpeed = 0.5f;

        // Chasing variables
        public float chaseSpeed = 1f;
        public GameObject target;

        // Fleeing variables
        public float fleeSpeed = 1.5f;
        GameObject[] enemies;
        private float shortestDistance;
        GameObject nearestEnemy;
        private float distanceToEnemy;

        void Start()
        {
            sight = GetComponent<LineOfSight>();
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<EnemyMovement>();
            player = GameObject.FindGameObjectWithTag("Player");
            stats = GetComponent<EnemyStats>();

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
                    case State.FLEE:
                        Flee();
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

        void Flee()
        {
            List<GameObject> enemies = new List<GameObject>();
            shortestDistance = Mathf.Infinity;
            nearestEnemy = null;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (enemies.Equals(this.gameObject))
                {
                    continue;
                    //enemies.Add(enemy); 
                }

                distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
            {
                agent.speed = fleeSpeed;
                agent.destination = nearestEnemy.transform.position;

                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                state = State.PATROL;
                target = null;
            }

            //if (distanceToEnemy <= 2 && sight.CanSeeTarget)     // WON'T WORK WHILE IT THINKS IT'S ITS OWN NEAREST ENEMY
            //{
            //    state = State.CHASE;
            //    target = player.gameObject;
            //    Chase();
            //}
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Time.timeScale = 0;
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                Time.timeScale = 1;
            }

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

            if (stats.health <= 50)
            {
                state = State.FLEE;
                target = nearestEnemy;
            }
        }
    }