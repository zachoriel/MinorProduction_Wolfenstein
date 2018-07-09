using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement functions
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    private Rigidbody rb;

    public bool beingAttacked;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Gets a movement vector
    public void Move(Vector3 _velocity, Vector3 _sVelocity)
    {
        if (beingAttacked)
        {
            velocity = _sVelocity;
        }
        else
        {
            velocity = _velocity;
        }
    }

    // Run every physics iteration
    void FixedUpdate()
    {
        PerformMovement();
    }

    //Perform movement based on velocity variable
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}
