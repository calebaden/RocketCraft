﻿using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour
{
    public float fuelAmount;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.CompareTag("Player"))
        {
            otherObject.GetComponent<PlayerController>().RefillFuel(fuelAmount);
            Destroy(gameObject);
        }
    }
}
