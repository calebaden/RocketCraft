using UnityEngine;
using System.Collections;

public class HoverScript : MonoBehaviour
{
    Rigidbody rigBody;

    int layerMask;
    public float hoverForce;
    public float hoverHeight;
    public GameObject[] hoverPoints;

    // Use this for initialization
    void Start ()
    {
        rigBody = GetComponent<Rigidbody>();
	}
	
    // FixedUpdate is called once every fixed amount of time
    void FixedUpdate ()
    {
        RaycastHit hit;
        foreach (GameObject hoverPoint in hoverPoints)
        {
            Vector3 downForce;
            float distancePercentage;

            if (Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hoverHeight))
            {
                distancePercentage = 1 - (hit.distance / hoverHeight);

                downForce = transform.up * hoverForce * distancePercentage;

                downForce = downForce * rigBody.mass;

                rigBody.AddForceAtPosition(downForce, hoverPoint.transform.position);
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
	
	}

    // Function returns true if the vehicle is upside down
    public bool IsStuck()
    {
        bool isStuck = false;

        foreach (GameObject hoverPoint in hoverPoints)
        {
            if (hoverPoint.transform.position.y > transform.position.y)
                isStuck = true;
        }

        return isStuck;
    }
}
