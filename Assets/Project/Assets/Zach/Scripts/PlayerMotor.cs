using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement functions
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    PlayerStats player;

    private Vector3 velocity = Vector3.zero;

    private Rigidbody rb;

    public bool beingAttacked;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerStats>();
    }

    // Gets a movement vector
    public void Move(Vector3 _velocity, Vector3 _sVelocity, Vector3 _fVelocity)
    {
        if (beingAttacked)
        {
            velocity = _sVelocity;
        }
        else
        {
            velocity = _velocity;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !beingAttacked)
        {
            velocity = _fVelocity;
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
