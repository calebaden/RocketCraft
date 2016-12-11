using UnityEngine;
using System.Collections;

public class ParticleEffectsScript : MonoBehaviour
{
    PlayerInputsScript playerInputs;

    ParticleSystem partSys;

    public float maxLifetime;
    public float minLifetime;
    public float maxRate;
    public float minRate;

	// Use this for initialization
	void Start ()
    {
        playerInputs = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInputsScript>();
        partSys = GetComponent<ParticleSystem>();
	}

    // Update is called once per frame
    void Update()
    {
        // Lerp the particle variables between themselves and the player acceleration input multiplies by a co efficient
        partSys.startLifetime = Mathf.Lerp(partSys.startLifetime, minLifetime + (playerInputs.acceleration * maxLifetime), 1);
        partSys.emissionRate = Mathf.Lerp(partSys.emissionRate, minRate + (playerInputs.acceleration * maxRate), 1);
    }
}
