using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    Rigidbody rb;
    public float speed;

    public bool toggle;

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
        /*if(Input.GetAxis("Horizontal")!= 0)
        {
            Debug.Log("This is the Horizontal Val: " + Input.GetAxis("Horizontal"));
        }
        if(Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("This is the Vertical Val: " + Input.GetAxis("Vertical"));
        }*/

        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

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

            if(Input.GetAxisRaw("Horizontal").Equals(0)&&Input.GetAxisRaw("Vertical").Equals(0))
            {
                rb.velocity = new Vector3(0,0,0);
            }

            //Press and hold code
            /*if(Input.GetKey(KeyCode.LeftControl))
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
            }*/
            
            
            //Toggle Code
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                toggle = !toggle;
            }

            //toggle crouch On
            if(toggle == true)
            {
                //camera.transform.position = camera.;


                gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            //toggle crouch off
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
            }

        }
    }
}
