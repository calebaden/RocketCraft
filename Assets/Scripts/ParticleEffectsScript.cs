using UnityEngine;
using System.Collections;

public class ParticleEffectsScript : MonoBehaviour
{
    PlayerController playerController;

    ParticleSystem partSys;

    public float maxLifetime;
    public float maxRate;

	// Use this for initialization
	void Start ()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        partSys = GetComponent<ParticleSystem>();
	}

    // Update is called once per frame
    void Update()
    {
        partSys.startLifetime = Mathf.Lerp(partSys.startLifetime, playerController.acceleration * maxLifetime, 1);
        partSys.emissionRate = Mathf.Lerp(partSys.emissionRate, playerController.acceleration * maxRate, 1);
    }
}
