using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    public GameObject correctObj;
    public GameObject wrongObj;
    public Text correctTxt;
    public Text wrongTxt;
    public static bool gameOverMenuIsActive = false;

    void Start()
    {
        // Disable everthing needed for the game over screen
        gameOverMenuIsActive = false;
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1f; // Start time
        PauseMenu.gameIsPaused = false;

        // Get objects and text
        correctObj = GameObject.Find("CorrectText");
        correctTxt = correctObj.GetComponent<Text>();
        wrongObj = GameObject.Find("WrongText");
        wrongTxt = wrongObj.GetComponent<Text>();
    }

    void Update()
    {
        // If any key is pressed but not mouse buttons, reload the scene on the same game mode e.g. easy, medium, hard.
        //  (The one they selected in the difficulty menu)
        if (Input.GetKey(KeyCode.Return) && !(Input.GetMouseButtonDown(0) 
            || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            if (PauseMenu.gameIsPaused && gameOverMenuIsActive && PlayerController.isDead)
            {
                QuestionController.correctCount = 0;
                QuestionController.wrongCount = 0;

                //Application.LoadLevel(Application.loadedLevel); // Old method for reloading scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        // If the player is dead variable is true go to gameover.
        if (PlayerController.isDead)
        {
            GameOver();
        }
    }

    // Activate everthing needed to display the game over screen
    void GameOver()
    {
        gameOverMenuIsActive = true;
        gameOverMenuUI.SetActive(true);
        PauseMenu.gameIsPaused = true;

        // Set text
        correctTxt.text = QuestionController.correctCount.ToString();
        wrongTxt.text = QuestionController.wrongCount.ToString();
    }
}
