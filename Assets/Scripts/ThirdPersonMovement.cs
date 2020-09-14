﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    public event Action Idle = delegate { };
    public event Action StartRunning = delegate { };
    public event Action StartJumping = delegate { };
    public event Action StartFalling = delegate { };
    public event Action Landing = delegate { };
    public event Action Sprinting = delegate { };
    public event Action Healing = delegate { };

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    [SerializeField] float sprintSpeed = 10f;
    public float turnSmoothTime = 0.1f;
    bool _isSprinting = false;

    bool _isJumping = false;
    public float jumpSpeed = 8f;
    public float gravity = 9.8f;
    private float vertSpeed = 0f;
    bool _isLanding = false;

    float turnSmoothVelocity;
    bool _isMoving = false;

    bool _isHealing = false;

    [SerializeField] AudioSource _healSound = null;


    private void Start()
    {
        Idle?.Invoke();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 vel = transform.forward * Input.GetAxis("Vertical") * speed;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        vertSpeed -= gravity * Time.deltaTime; // apply gravity to vertical speed;
        vel.y = vertSpeed; //include vertical speed in velocity
        controller.Move(vel * Time.deltaTime);

        if (controller.isGrounded) // Only Jump If On the Ground.
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
                if (controller.isGrounded == false) // Only Play Jump Animation In The Air
                {
                    OnJumping();
                }
                else if (controller.isGrounded) // Play Landing Animation After Jump But On The Ground
                {
                    Land();
                }
            }
        }

        if (direction.magnitude >= 0.1f) // if the character is moving
        {

            if (controller.isGrounded) // if the character is on the ground
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);

                vertSpeed = 0; // Don't move the character off the ground.

                if (Input.GetKey(KeyCode.Space))
                {
                    Jump();
                    if (controller.isGrounded == false)
                    {
                        OnJumping();
                    }
                    else if (controller.isGrounded)
                    {
                        Land();
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Sprint();
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        _isHealing = true;
                        OnHealing();
                    }
                    else
                    {
                        _isSprinting = true;
                    }
                    
                }

            }

            CheckIfStartedMoving();

        }
        else
        {
            CheckIfStoppedMoving();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isHealing = true;
            OnHealing();
        }
    }

    private void Jump()
    {
        Debug.Log("Jump Pressed");
        vertSpeed = jumpSpeed; // Jump if the Space Bar is Pressed
        _isJumping = true;
    }

    private void Land()
    {
        Debug.Log("Land");
        OnLanding();
        _isLanding = true;
    }

    private void Sprint()
    {
        Debug.Log("Sprint");
        OnSprinting();
        speed = sprintSpeed;
    }

    private void CheckIfStartedMoving()
    {
        if(_isMoving == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isHealing = true;
                OnHealing();
            } else
            {
                StartRunning?.Invoke();
                Debug.Log("Started");
            }
        }
        _isMoving = true;
    }

    private void CheckIfStoppedMoving()
    {
        if(_isMoving == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isHealing = true;
                OnHealing();
            }
            else
            {
                Idle?.Invoke();
                Debug.Log("Stopped");
            }
            
        }
        _isMoving = false;
    }

    private void OnJumping()
    {
        if(_isJumping == true)
        {
            StartJumping?.Invoke();
            Debug.Log("Jumping");
        }

        _isJumping = false;
    }

    private void OnLanding()
    {
        if(_isLanding == true)
        {
            Landing?.Invoke();
            Debug.Log("Landed");
        }

        _isLanding = false;
    }

    private void OnSprinting()
    {
        if(_isSprinting == true)
        {
            Sprinting?.Invoke();
        }
    }

    private void OnHealing()
    {
        
        if(_isHealing == true)
        {
            _healSound.Play();
            Healing?.Invoke();
        }
    }
}
