using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    PlayerInputsScript playerInputs;

    public AudioSource gasAudioSource;
    public AudioSource flameAudioSource;
    public AudioClip checkpoint;
    public AudioClip countdown;
    public AudioClip go;
    public AudioClip pickup;
    public AudioClip[] collisions;

    float minGasVol = 0.1f;
    float gasVolCoEff = 0.3f;
    float minGasPitch = 1;
    float gasPitchCoEff = 0.1f;
    float flameVolCoEff = 0.2f;
    float lerpSpeed = 0.5f;

	// Use this for initialization
	void Start ()
    {
        playerInputs = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInputsScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Lerp the engine sounds between themselves and the players acceleration input multiplied by a co efficient
        gasAudioSource.volume = Mathf.Lerp(gasAudioSource.volume, minGasVol + (playerInputs.acceleration * gasVolCoEff), lerpSpeed);
        gasAudioSource.pitch = Mathf.Lerp(gasAudioSource.pitch, minGasPitch + (playerInputs.acceleration * gasPitchCoEff), lerpSpeed);
        flameAudioSource.volume = Mathf.Lerp(flameAudioSource.volume, playerInputs.acceleration * flameVolCoEff, lerpSpeed);
    }
}
