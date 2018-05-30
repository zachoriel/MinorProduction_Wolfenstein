using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [Header("Component Setup")]
    public GameObject thePlayer;

    [Header("Detection")]
    public float HoodRadius;
    public float range;

    [HideInInspector]
    public bool CanSeeTarget = false;
    //public Transform gameObject = null;


    void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        int hoodSize = 0;
        Collider[] hood = Physics.OverlapSphere(transform.position, HoodRadius);
        CanSeeTarget = false;
        foreach(Collider guyInHood in hood)
        {
            hoodSize++;
            if (guyInHood.transform.GetComponent<PlayerStats>())
            {
                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, (thePlayer.transform.position - gameObject.transform.position).normalized, out hit, range))
                {
                    //Debug.Log(hit.transform.name);
                    //checks to see if an Abstract script named "Player" exists on the object colliding with this raycast
                    PlayerStats player = hit.transform.GetComponent<PlayerStats>();
                    if (player != null)
                    {
                        CanSeeTarget = true;
                    }
                    else if (player == null)
                    {
                        //CanSeeTarget = false;
                    }
                }
            }
            else if(!guyInHood.transform.GetComponent<PlayerStats>())
            {
               // CanSeeTarget = false;
            }
        }
    }

        
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;
        //Gizmos.DrawRay(gameObject.transform.position, (thePlayer.transform.position - gameObject.transform.position).normalized);
        Gizmos.DrawSphere(gameObject.transform.position, HoodRadius);

    }
}