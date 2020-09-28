using System.Collections;
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
    public event Action Hovering = delegate { };
    public event Action Dead = delegate { };

    public CharacterController controller;
    public Transform cam;
    [SerializeField] Health _playerHealth = null;

    public float speed = 6f;
    [SerializeField] float sprintSpeed = 10f;
    public float turnSmoothTime = 0.1f;
    bool _isSprinting = false;

    bool _isJumping = false;
    public float jumpSpeed = 3f;
    public float gravity = 9.8f;
    private float vertSpeed = 0f;
    bool _isLanding = false;
    bool _isFalling = false;
    bool _isFloating = false;

    float turnSmoothVelocity;
    bool _isMoving = false;

    bool _isHealing = false;

    bool _isDead = false;

    [SerializeField] AudioSource _healSound = null;
    [SerializeField] AudioSource _jumpSound = null;
    [SerializeField] AudioSource _deadSound = null;


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
                
                    OnJumping();

                if (!controller.isGrounded)
                {
                    Fall();
                }
                if (controller.velocity.y < 0)
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
                        OnJumping();
                            if (controller.velocity.y < 0)
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
            if(controller.isGrounded == false)
            {
                gravity = 0;
            }
            _isHealing = true;
            OnHealing();
        }

        if (!Input.GetKey(KeyCode.Mouse0))
        {
            gravity = 9.8f;
            
            _isFloating = false;
        }
    }

    private void Jump()
    {
        Debug.Log("Jump Pressed");
        vertSpeed = jumpSpeed; // Jump if the Space Bar is Pressed
        _isJumping = true;
        _jumpSound.Play();
        OnJumping();
    }

    private void Fall()
    {
        Debug.Log("Falling.");
        _isFalling = true;
        OnFalling();
    }
    

    private void Land()
    {
        _isJumping = false;
        _isMoving = false;
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
        if (_playerHealth._currentHealth <= 0)
        {
            Debug.Log("Call On Dead.");
            speed = 0;
            OnDead();
            _isSprinting = false;

            _isJumping = false;
            _isLanding = false;
            _isFalling = false;
            _isFloating = false;
            _isMoving = false;
            _isHealing = false;
            _isDead = false;
        }

        if (_isMoving == false)
        {
            if(_isJumping == true)
            {
                StartJumping?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isHealing = true;
                OnHealing();

                if(controller.isGrounded == false)
                {
                    _isFloating = true;
                    OnHovering();
                }
            }
            else
            {
                StartRunning?.Invoke();
                Debug.Log("Started");
            }
        }
        _isMoving = true;
    }

    private void CheckIfStoppedMoving()
    {
        if (_playerHealth._currentHealth <= 0)
        {
            Debug.Log("Call On Dead.");
            speed = 0;
            OnDead();
            _isSprinting = false;

            _isJumping = false;
            _isLanding = false;
            _isFalling = false;
            _isFloating = false;
            _isMoving = false;
            _isHealing = false;
            _isDead = false;
        }
        if (_isMoving == true)
        {
            if(_isJumping == true)
            {
                StartJumping?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isHealing = true;
                OnHealing();
                if (controller.isGrounded == false)
                {
                    _isFloating = true;
                    OnHovering();
                }
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
    }

    private void OnFalling()
    {
        if(_isFalling == true)
        {
            StartFalling?.Invoke();
        }
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

    private void OnHovering()
    {
        if(_isFloating == true)
        {
            Hovering?.Invoke();
        }
    }

    public void OnDead()
    {
            Dead?.Invoke();
            speed = 0;
    }
}
