using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
    GameController gameController;
    PlayerInputsScript playerInputs;
    PlayerController playerController;

    public string currentScene;

    [Header("Main Menu UI")]
    public Button playButton;
    public Button creditsButton;
    public Button quitButton;
    public Image selection;

    [Header("Level UI")]
    public Text currentTime;
    public Text countDown;
    public Slider fuelSlider;
    public Text checkpointTimeText;
    public Text splitTimeText;

    float goTimer = 1;
    float selTimer = 0.2f;
    float selReset = 0.2f;
    int currentSelection = 0;
    int maxSelection = 2;
    int minSelection = 0;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerInputs = gameController.gameObject.GetComponent<PlayerInputsScript>();

        if (currentScene == "Level")
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentScene == "MainMenu")
        {
            MainMenuUI();
        }
        else if (currentScene == "Level")
        {
            LevelUI();
        }
	}

    // Function that handles main menu UI
    void MainMenuUI ()
    {
        if (selTimer > 0)
        {
            selTimer -= Time.deltaTime;
        }

        // Increment or decrement current selection relative to player input
        if (playerInputs.vertical > 0 && selTimer <= 0)
        {
            selTimer = selReset;
            currentSelection--;
        }
        else if (playerInputs.vertical < 0 && selTimer <= 0)
        {
            currentSelection++;
            selTimer = selReset;
        }

        // Clamp current selection between 0 and 4
        if (currentSelection > maxSelection)
        {
            currentSelection = maxSelection;
        }
        else if (currentSelection < minSelection)
        {
            currentSelection = minSelection;
        }

        float buttonDist = -150;
        Vector3 selPosition = selection.rectTransform.localPosition;
        selPosition.y = currentSelection * buttonDist;
        selection.rectTransform.localPosition = selPosition;

        if (Input.GetButtonDown("Select"))
        {
            // Player has Play selected, load level
            if (currentSelection == 0)
            {
                SceneManager.LoadScene("Test");
            }
            // Player has Credits selected, display credits
            else if (currentSelection == 1)
            {
                // Load Credits
            }
            // Player has Quit selected, quit the application
            else
            {
                Application.Quit();
            }
        }
    }

    // Function that handles level UI
    void LevelUI ()
    {
        if (gameController.isCounting)
        {
            countDown.text = gameController.countDown.ToString();
        }
        else
        {
            goTimer -= Time.deltaTime;
            countDown.text = "GO!";
            if (goTimer <= 0)
            {
                countDown.text = "";
                // Make this stop getting called
            }

            currentTime.text = "Time" + "\n" + gameController.currentTime.ToString("F2");
        }

        fuelSlider.value = playerController.fuel;
    }

    public void CheckPointCoRo (float checkpointTime, float splitTime)
    {
        StartCoroutine(CheckpointUI(checkpointTime, splitTime));
    }

    // Coroutine that displays the checkpoint and split time UI
    IEnumerator CheckpointUI (float checkpointTime, float splitTime)
    {
        checkpointTimeText.enabled = true;
        splitTimeText.enabled = true;
        if (splitTime <= 0)
        {
            splitTimeText.color = Color.green;
        }
        else
        {
            splitTimeText.color = Color.red;
        }
        checkpointTimeText.text = checkpointTime.ToString("F2");
        splitTimeText.text = splitTime.ToString("F2");
        yield return new WaitForSeconds(2);
        checkpointTimeText.enabled = false;
        splitTimeText.enabled = false;
        yield return null;
    }
}
