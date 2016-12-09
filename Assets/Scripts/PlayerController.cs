using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public float acceleration;
    float horizontal;
    float vertical;
    bool isGrounded;
    float fuel = 100;

    public float maxAcceleration;
    public float steerSpeed;
    public float yawSpeed;
    public float pitchSpeed;
    public float rollSpeed;
    public bool canRoll;
    public float maxVelocity;
    public float fuelCoefficient;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
    
    // Fixed Update is called every fixed amount of time
    void FixedUpdate ()
    {
        if (acceleration != 0 && fuel > 0)
        {
            rb.AddForce(transform.forward * (maxAcceleration * acceleration));

            if (!isGrounded)
            {
                fuel -= acceleration * fuelCoefficient;
            }

            Debug.Log(fuel);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }
    // Update is called once per frame
    void Update ()
    {
        acceleration = Input.GetAxis("Acceleration");
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        canRoll = Input.GetButton("AirRoll");

        // Check if the player is grounded and call the appropriate function
        if (isGrounded)
        {
            GroundControls();
        }
        else
        {
            AirControls();
        }
	}

    // Function that controls ground movement
    void GroundControls ()
    {
        // Rotate around the Y axis
        transform.Rotate(0, (steerSpeed * horizontal) * Time.deltaTime, 0, Space.Self);
    }

    // Function taht controls air movement
    void AirControls ()
    {
        // Rotate around the X axis
        transform.Rotate((pitchSpeed * vertical) * Time.deltaTime, 0, 0, Space.Self);

        if (canRoll)
        {
            // Rotate around the Z axis
            transform.Rotate(0, 0, (rollSpeed * horizontal) * Time.deltaTime, Space.Self);
        }
        else
        {
            // Rotate around the Y axis
            transform.Rotate(0, (yawSpeed * horizontal) * Time.deltaTime, 0, Space.Self);
       }
    }

    public void RefillFuel (float amount)
    {
        fuel += amount;
    }

    void OnCollisionEnter (Collision otherObject)
    {
        if (otherObject.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit (Collision otherObject)
    {
        if (otherObject.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
