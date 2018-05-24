using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyMovement : MonoBehaviour
{
    private Transform target;
    public int wavepointIndex;
    public int speed;
    public int startSpeed = 10;
    public int startWaypoint;
    public int endPathPoint;

	// Use this for initialization
	void Start ()
    {
        wavepointIndex = startWaypoint;
        target = Waypoints.waypoints[startWaypoint];
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        speed = startSpeed;
	}

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - endPathPoint)                // This is a shitty solution to an awkward problem. If you're confused and need to use this script, I'll explain it in person.
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }

    void EndPath()
    {
        wavepointIndex = startWaypoint;
        target = Waypoints.waypoints[startWaypoint];

    }
}
