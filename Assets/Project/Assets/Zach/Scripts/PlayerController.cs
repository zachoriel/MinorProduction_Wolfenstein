using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles inputs
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public WeaponSwitch weapons;
    public Animator MGanimator;
    public Animator SGanimator;
    public Animator LRanimator;

    [SerializeField]
    private float speed = 5f;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMove;
        Vector3 _moveVertical = transform.forward * _zMove;

        // Final movement vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        // Apply movement
        motor.Move(_velocity);
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal").Equals(0) && Input.GetAxisRaw("Vertical").Equals(0))
        {
            if (weapons.selectedWeapon == 0 && weapons.hasPickedUpGun == true)
            {
                MGanimator.SetBool("isRunning", false);
            }
            else if (weapons.selectedWeapon == 1 && weapons.hasPickedUpGun == true)
            {
                SGanimator.SetBool("isRunning", false);
            }
            else if (weapons.selectedWeapon == 2 && weapons.hasPickedUpGun == true)
            {
                LRanimator.SetBool("isRunning", false);
            }
        }
        else
        {
            if (weapons.selectedWeapon == 0 && weapons.hasPickedUpGun == true)
            {
                MGanimator.SetBool("isRunning", true);
            }
            else if (weapons.selectedWeapon == 1 && weapons.hasPickedUpGun == true)
            {
                SGanimator.SetBool("isRunning", true);
            }
            else if (weapons.selectedWeapon == 2 && weapons.hasPickedUpGun == true)
            {
                LRanimator.SetBool("isRunning", true);
            }
        }
    }
}
