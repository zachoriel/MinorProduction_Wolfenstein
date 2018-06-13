using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public LineOfSight sight;

    public GameObject player;

    public GameObject target;

    Rigidbody rb;


    public float rad;
    float timer;
    float angle;
    float speed;
    float width;

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
            /*target = player.gameObject;
            timer += Time.deltaTime;
            float x = Mathf.Cos(timer - (new Vector3( player.transform.position)));
            float y = Mathf.Sin(timer);
            float z = 0;

            transform.position = new Vector3(x, y, z);*/
            //transform.LookAt(target.transform.position);
        }
    }
}
