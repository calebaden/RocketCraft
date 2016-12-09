using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    HoverScript hoverScript;

    Rigidbody rigBody;

    bool isGrounded;

    public float maxAcceleration;
    public float turnForce;
    public float yawForce;
    public float pitchForce;
    public float rollForce;
    public float acceleration;
    float horizontal;
    float vertical;
    bool canRoll;

    // Used for initialization
    void Start ()
    {
        hoverScript = GetComponent<HoverScript>();

        rigBody = GetComponent<Rigidbody>();
        rigBody.maxAngularVelocity = 5;
    }

    // Update is called once every frame
    void Update()
    {
        // Player Inputs
        acceleration = Input.GetAxis("Acceleration");
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        canRoll = Input.GetButton("AirRoll");

        if (Input.GetButton("Help"))
        {
            if (hoverScript.IsStuck())
            {
                transform.Translate(Vector3.down * 10 * Time.deltaTime);
                transform.Rotate(Vector3.forward * 200 * Time.deltaTime);
            }
        }
    }

    // Fixed update is called once every fixed amount of time
    void FixedUpdate ()
    {
        // Check if the vehicle is within the grounded distance
        if (Physics.Raycast(transform.position, -transform.up, 3f))
        {
            rigBody.drag = 1;
            GroundedControls();
        }
        else
        {
            rigBody.drag = 0.5f;
            AirborneControls();
        }

        // Forward
        if (acceleration > 0)
            rigBody.AddForce(transform.forward * acceleration * maxAcceleration);

        if (horizontal == 0 && vertical == 0)
        {
            float lerpSpeed = 0.1f;
            rigBody.angularVelocity = Vector3.Lerp(rigBody.angularVelocity, Vector3.zero, lerpSpeed);
        }
    } 

    // Function that controls the players rotations while grounded
    void GroundedControls ()
    {
        // Horizontal
        if (horizontal != 0)
            // Rotate around the Y axis
            rigBody.AddRelativeTorque(Vector3.up * horizontal * turnForce);
    }

    // Function that controls the players rotations while not grounded
    void AirborneControls ()
    {
        // Horizontal
        if (horizontal != 0)
        {
            // If the air roll button is pressed, rotate around the Z axis
            if (canRoll)
                rigBody.AddRelativeTorque(Vector3.back * horizontal * rollForce);
            // If the air roll button is not pressed, rotate around the Y axis
            else
                rigBody.AddRelativeTorque(Vector3.up * horizontal * yawForce);
        }

        // Pitch
        if (vertical != 0)
            // Rotate around the X axis
            rigBody.AddRelativeTorque(Vector3.right * vertical * pitchForce);
    }

    public void RefillFuel(float amount)
    {

    }
}
