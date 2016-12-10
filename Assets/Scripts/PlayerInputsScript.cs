using UnityEngine;
using System.Collections;

public class PlayerInputsScript : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float acceleration;
    public bool canRoll;
	
	// Update is called once per frame
	void Update ()
    {
        acceleration = Input.GetAxis("Acceleration");
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        canRoll = Input.GetButton("AirRoll");
    }
}
