using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    Rigidbody rb;
    public float speed;
    Vector3 forwardDir;
    Vector3 rightDir;

    //Vector3 CameraYrot;

    Transform camera;
    void Start()
    {
        // Debug.Log(transform.childCount);
        // CameraX = this.gameObject.transform.GetChild(0);
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        // Debug.Log(CameraX.transform.childCount);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal")!= 0)
        {
            Debug.Log("This is the Horizontal Val: " + Input.GetAxis("Horizontal"));
        }
        if(Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("This is the Vertical Val: " + Input.GetAxis("Vertical"));
        }

        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (camera != null)
        {
            forwardDir = camera.transform.forward * vert;
            rightDir = camera.transform.right * horz;
            gameObject.transform.rotation = camera.transform.rotation;
        }
    }
    void FixedUpdate()
    {
        //Debug.Log((forwardDir + rightDir) * speed );
        //rb.AddForce((forwardDir + rightDir) * speed );
        //rb.AddForce((forwardDir + rightDir) * speed);

        if (camera != null)
        {

            Vector3 dir = (forwardDir + rightDir).normalized;
            Vector3 desiredVel = dir * speed;
            rb.AddForce(desiredVel - rb.velocity);

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }
}
