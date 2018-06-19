using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    public enum State
    {
        PATROL,
        CHASE,
        FLEE
    }

    [Header ("Script Setup")]
    public LineOfSight sight;
    public EnemyMovement character;
    public EnemyStats stats;

    [Header ("Component Setup")]
    public NavMeshAgent agent;
    public LineRenderer laser;

    [Header ("State Machine")]
    public State state;
    public bool isAlive;

    [Header ("Patrolling variables")]
    public GameObject[] waypoints;
    public int waypointIndex = 0;
    public float patrolSpeed = 10f;

    [Header ("Chasing variables")]
    public GameObject target;
    public GameObject player;
    public float chaseSpeed = 20f;

    [Header ("Fleeing variables")]
    public float fleeSpeed = 30f;
    public float fleeThreshold = 20;
    GameObject[] enemies;
    GameObject nearestEnemy;
    private float shortestDistance;
    public float distanceToEnemy;
    public bool hasReachedEnemy = false;

    [Header ("Drone Spawning")]
    public GameObject DronePrefab;
    public GameObject instantiated;
    public Transform me;
    public bool canInstantiate;
    public int instantiateValue;
        

    void Start()
    {
        sight = GetComponent<LineOfSight>();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<EnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<EnemyStats>();
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
        //if (instantiateValue < 3)
        //{
        //    instantiated = Instantiate(DronePrefab, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y, transform.position.z),
        //        Quaternion.identity, gameObject.transform);
        //    instantiateValue++;
        //}

        agent.speed = chaseSpeed;
        agent.destination = target.gameObject.transform.position;
            
        character.Move(agent.desiredVelocity, false, false);
    }

    void Flee()
    {
        List<GameObject> enemies = new List<GameObject>();
        shortestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (GameObject enemy in BigListOfEnemies.instance.enemyList)
        {
            distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= fleeThreshold) { continue; }
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
    }

    void Update()
    {

        if (sight.CanSeeTarget && state != State.FLEE)  
        {
            state = State.CHASE;
            target = player.gameObject;
            Chase();
        }
        else if (!sight.CanSeeTarget && state != State.FLEE)
        {               
            state = State.PATROL;
            target = null;             
        }

        //if (stats.health <= 50 && state != State.FLEE)
        //{
        //    state = State.FLEE;
        //    target = nearestEnemy;
        //}

        if (distanceToEnemy <= 5 && nearestEnemy != null)
        {
            hasReachedEnemy = true;
        }

        if (hasReachedEnemy)
        {
            state = State.CHASE;
            target = player.gameObject;
            nearestEnemy = null;
            Chase();
        }
    }
}