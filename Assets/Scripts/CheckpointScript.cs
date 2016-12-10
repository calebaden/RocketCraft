using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour
{
    GameController gameController;
    UIController uiController;

    public int thisCheckpoint;
    public float checkpointGoal;
    public float bestCheckpointTime;

    float checkpointTime;
    float splitTime;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        bestCheckpointTime = 1000;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // This function is called whenever this object is triggered by another collider
    void OnTriggerEnter (Collider otherObject)
    {
        if (otherObject.CompareTag("Player"))
        {
            if (gameController.previousCheckpoint == thisCheckpoint - 1)
            {
                gameController.previousCheckpoint = thisCheckpoint;

                checkpointTime = gameController.currentTime;

                splitTime = checkpointTime - Mathf.Min(checkpointGoal, bestCheckpointTime);

                Debug.Log(bestCheckpointTime);

                // If the new time is less than the goal, set best time as the new time
                if (checkpointTime < bestCheckpointTime)
                {
                    bestCheckpointTime = checkpointTime;
                }
                else if (checkpointTime < checkpointGoal)
                {
                    bestCheckpointTime = checkpointTime;
                }

                uiController.CheckPointCoRo(checkpointTime, splitTime);
            }
        }
    }
}
