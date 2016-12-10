using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    GameController gameController;
    PlayerInputsScript playerInputs;

    Rigidbody rigBody;

    public GameObject sparkPrefab;

    bool isGrounded;
    float maxAcceleration;

    [Header("Stuff Here")]
    public float groundAcceleration;
    public float airAcceleration;
    public float turnForce;
    public float yawForce;
    public float pitchForce;
    public float rollForce;
    public float fakeRoll;

    public float fuel;
    float maxFuel = 100;
    float fuelDrain = 20;
    float fuelGain = 0.05f;

    // Used for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerInputs = gameController.gameObject.GetComponent<PlayerInputsScript>();

        rigBody = GetComponent<Rigidbody>();
        rigBody.maxAngularVelocity = 5;

        turnForce *= rigBody.mass;
        yawForce *= rigBody.mass;
        pitchForce *= rigBody.mass;
        rollForce *= rigBody.mass;
        fakeRoll *= rigBody.mass;
    }

    // Update is called once every frame
    void Update()
    {
        // Drain the
        if (playerInputs.acceleration > 0 && fuel > 0 && !isGrounded)
        {
            fuel -= (playerInputs.acceleration * fuelDrain) * Time.deltaTime;
        }
        else if (isGrounded)
        {
            RefillFuel(fuelGain);
        }
    }

    // Fixed update is called once every fixed amount of time
    void FixedUpdate ()
    {
        if (gameController.isStalled)
        {
            rigBody.angularVelocity = Vector3.Lerp(rigBody.angularVelocity, Vector3.zero, 1);
            rigBody.velocity = Vector3.Lerp(rigBody.velocity, Vector3.zero, 1);
        }
        else
        {
            // Check if the vehicle is within the grounded distance
            if (Physics.Raycast(transform.position, -transform.up, 3f))
            {
                isGrounded = true;
                rigBody.drag = 1;
                maxAcceleration = groundAcceleration * rigBody.mass;
                GroundedControls();
            }
            else
            {
                isGrounded = false;
                rigBody.drag = 0.1f;
                maxAcceleration = airAcceleration * rigBody.mass;
                AirborneControls();
            }

            // Forward
            if (playerInputs.acceleration > 0 && fuel > 0)
            {
                rigBody.AddForce(transform.forward * playerInputs.acceleration * maxAcceleration);
            }
        }
    } 

    // Function that controls the players rotations while grounded
    void GroundedControls ()
    {
        // Horizontal
        if (playerInputs.horizontal != 0)
        {
            // Rotate around the Y axis
            rigBody.AddRelativeTorque(Vector3.up * playerInputs.horizontal * turnForce);

            rigBody.AddRelativeTorque(Vector3.back * playerInputs.horizontal * fakeRoll);
        }

        if (playerInputs.horizontal == 0)
        {
            rigBody.angularVelocity = Vector3.Lerp(rigBody.angularVelocity, Vector3.zero, 0.2f);
        }
        else
        {
            rigBody.angularVelocity = Vector3.Lerp(rigBody.angularVelocity, Vector3.zero, 0.1f);
        }
    }

    // Function that controls the players rotations while not grounded
    void AirborneControls ()
    {
        // Horizontal
        if (playerInputs.horizontal != 0)
        {
            // If the air roll button is pressed, rotate around the Z axis
            if (playerInputs.canRoll)
            {
                rigBody.AddRelativeTorque(Vector3.back * playerInputs.horizontal * rollForce);
            }

            // If the air roll button is not pressed, rotate around the Y axis
            else
            {
                rigBody.AddRelativeTorque(Vector3.up * playerInputs.horizontal * yawForce);
            }
        }

        // Pitch
        if (playerInputs.vertical != 0)
        {
            // Rotate around the X axis
            rigBody.AddRelativeTorque(Vector3.right * playerInputs.vertical * pitchForce);
        }
        else
        {
            // Add brake torque to the pitch axis
            Vector3 newPitchVelocity = new Vector3(0, rigBody.angularVelocity.y, rigBody.angularVelocity.z);
            rigBody.angularVelocity = Vector3.Lerp(rigBody.angularVelocity, newPitchVelocity, 0.1f);
        }

        if (playerInputs.canRoll)
        {
            // Add brake torque to the yaw axis
            Vector3 newYawVelocity = new Vector3(rigBody.angularVelocity.x, 0, rigBody.angularVelocity.z);
            rigBody.angularVelocity = Vector3.Lerp(rigBody.angularVelocity, newYawVelocity, 0.1f);
        }
        else
        {
            // Add brake torque to the roll axis
            Vector3 newRollVelocity = new Vector3(rigBody.angularVelocity.x, rigBody.angularVelocity.y, 0);
            rigBody.angularVelocity = Vector3.Lerp(rigBody.angularVelocity, newRollVelocity, 0.1f);
        }

        // Add brake torque to all axes while there are no vertical or horizontal inputs
        if (playerInputs.horizontal == 0 && playerInputs.vertical == 0)
        {
            rigBody.angularVelocity = Vector3.Lerp(rigBody.angularVelocity, Vector3.zero, 0.1f);
        }
    }

    // Function that adds a parameter to the fuel variable
    public void RefillFuel(float amount)
    {
        fuel += amount;

        if (fuel > maxFuel)
        {
            fuel = maxFuel;
        }
    }

    // Function is called when entering a collider
    void OnCollisionEnter (Collision otherObject)
    {
        // Instantiate the spark prefab as a game object on collision and delete it after a set time
        foreach (ContactPoint contact in otherObject.contacts)
        {
            float lifeTime = 1;
            Quaternion playerRot = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, 0);
            GameObject spark = (GameObject)Instantiate(sparkPrefab, contact.point, playerRot);
            Destroy(spark, lifeTime);
        }
    }
}
