using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    UIController uiController;

    public float currentTime;
    public int currentMedal;
    public int previousCheckpoint;
    public int countDown = 3;

    public bool isCounting;
    public bool isStalled;
    float timer = 1;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        isStalled = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (uiController.currentScene == "MainMenu")
        {
            MainMenuScene();
        }
        else if (uiController.currentScene == "Level")
        {
            LevelScene();
        }
    }

    // Function that handles main menu systems
    void MainMenuScene ()
    {

    }

    // Function that handles in level systems
    void LevelScene ()
    {
        if (isCounting)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                countDown--;
                timer = 1;
            }

            if (countDown <= 0)
            {
                isCounting = false;
            }
        }
        else
        {
            if (isStalled)
            {
                isStalled = false;
            }

            currentTime += Time.deltaTime;
        }
    }

    // When a new scene is loaded, get the current UI controller
    void OnLevelWasLoaded ()
    {
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }
}
