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
        partSys.startLifetime = Mathf.Lerp(partSys.startLifetime, (playerInputs.acceleration * maxLifetime) + minLifetime, 1);
        partSys.emissionRate = Mathf.Lerp(partSys.emissionRate, (playerInputs.acceleration * maxRate) + minRate, 1);
    }
}
