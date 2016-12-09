using UnityEngine;
using System.Collections;

public class ParticleEffectsScript : MonoBehaviour
{
    PlayerController playerController;

    ParticleSystem partSys;

    public float minRate;
    public float minLifetime;

	// Use this for initialization
	void Start ()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        partSys = GetComponent<ParticleSystem>();
	}

    // Update is called once per frame
    void Update()
    {
        partSys.startLifetime = Mathf.Lerp(partSys.startLifetime, playerController.acceleration, 1);
        partSys.emissionRate = Mathf.Lerp(partSys.emissionRate, playerController.acceleration * 1500, 1);
    }
}
