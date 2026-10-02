using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction mLook, mMove, mDash, mSprint, mStand;
    private Rigidbody rb;

    private float xlook, ylook;

    public Camera Camera;
    public float Force = 80f;

    public float Acceleration = 0f, Velocity = 0f;

    public bool isGrounded = true;

    public float MaximumSpeed = 8f;

    // Serving for rolling wheels
    public float leftWheelVelocity, rightWheelVelocity;

    // Dash
    public bool canDash = false;
    public float dashCoolDown = 3f; // Dash for 0.2s, then 3s cool down

    // Sprint
    public bool isSprinting = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        mLook = InputSystem.actions.FindAction("Look");
        mMove = InputSystem.actions.FindAction("Move");
        mDash = InputSystem.actions.FindAction("Dash");
        mSprint = InputSystem.actions.FindAction("Sprint");
        mStand = InputSystem.actions.FindAction("Stand");

        xlook = 90f;
        ylook = 20f;

        leftWheelVelocity = rightWheelVelocity = 0;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Acceleration = Force / rb.mass;

    }

    void FixedUpdate()
    {
        Vector2 look = mLook.ReadValue<Vector2>();
        Vector2 move = mMove.ReadValue<Vector2>();

        isGrounded = transform.position.y < 0.8f? true: false;
        
        // Maximum Speed
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, MaximumSpeed);

        // Moving Robot
        if(isGrounded)
        {
            rb.AddForce(transform.right * move.y * Force, ForceMode.Force);
            Velocity = Vector3.Dot(rb.linearVelocity, transform.right) >= 0 ? rb.linearVelocity.magnitude : -rb.linearVelocity.magnitude;// Get scale of velocity
            rb.linearVelocity = transform.right * Velocity;
            transform.Rotate(Vector3.up * move.x * Time.fixedDeltaTime * 100f);
        }

        // [OPT 1]Moving Camera(Free Camera, robot is not controler by it)
        /*
        xlook += look.x * 0.1f;
        ylook -= look.y * 0.5f;
        ylook = Math.Clamp(ylook, -80, 50);
        Camera.transform.position = transform.position + transform.TransformDirection(new Vector3(-2.5f, 2.79f, 0f));
        Camera.transform.localRotation = Quaternion.AngleAxis(xlook, Vector3.up) * Quaternion.AngleAxis(ylook, Vector3.right);
        */

        // [OPT 2]Using mouse moving robot direction
        // Camera.transform.parent = transform;
        xlook = look.x * 0.1f;
        ylook += look.y * 0.3f;
        ylook = Math.Clamp(ylook, -80, 50);
        if(isGrounded) transform.Rotate(Vector3.up * xlook * Time.fixedDeltaTime * 100f);
        Camera.transform.localRotation = Quaternion.AngleAxis(ylook, Vector3.forward) * Quaternion.AngleAxis(90f, Vector3.up);

        // Rolling Wheels
        // Problem with rotating robot, it's rotating transform, instead of rigidbody
        Velocity = Vector3.Dot(rb.linearVelocity, transform.right) >= 0 ? rb.linearVelocity.magnitude : -rb.linearVelocity.magnitude;
        leftWheelVelocity = Velocity + move.x + look.x * 0.2f; // Adjust rolling speed with input from mouse and keyboard
        rightWheelVelocity = -Velocity + (move.x + look.x * 0.2f);


        // Dashing
        if(mDash.IsPressed() && isGrounded && canDash)
        {
            rb.AddForce(transform.right * 1000f, ForceMode.Acceleration);
            dashCoolDown = 3f;
            canDash = false;
        }

        if(dashCoolDown>0f)
        {
            dashCoolDown -= Time.fixedDeltaTime;
        }else
        {
            dashCoolDown = 0f;
            canDash = true;
        }

        // Sprint
        if (mSprint.IsPressed())
        {
            isSprinting = true;
            if(move.y >= 0)
            {
                rb.AddForce(transform.right * 2f, ForceMode.VelocityChange);
            }else
            {
                rb.AddForce(transform.right * -2f, ForceMode.VelocityChange);
            }
            
        }else
        {
            isSprinting = false;
        }

        // Stand up, if robot is lying on the ground
        if(mStand.IsPressed() && rb.linearVelocity.magnitude <= 0.001f)
        {
            transform.up = Vector3.up;
        }


    }


}
