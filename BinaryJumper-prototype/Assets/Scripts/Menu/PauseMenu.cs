using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool gameIsPaused = false;
    public static bool pauseMenuIsActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // (Normal resume) If game is paused, pause menu is active and question menu is not active, then resume game on next key down.
            if (gameIsPaused && pauseMenuIsActive && !QuestionMenu.questionMenuIsActive)
            {
                Resume();
            }
            // (Resume into question menu) If game is paused, pause menu is active and question menu is acitve, then resume but keep time paused.
            else if (gameIsPaused && pauseMenuIsActive && QuestionMenu.questionMenuIsActive)
            {
                ResumeQuestionMenu();
            }
            // If game is paused and any other menus aren't active, activate pause menu.
            else if (!gameIsPaused && !QuestionMenu.questionMenuIsActive || !GameOverMenu.gameOverMenuIsActive)
            {
                Pause();
            }
        }

        if (gameIsPaused)
        {
            Cursor.visible = true;
        }
        else if (!gameIsPaused)
        {
            Cursor.visible = false;
        }
    }

    public void Resume()
    {
        // If question menu is not active, resume the game normally.
        if (!QuestionMenu.questionMenuIsActive)
        {
            pauseMenuIsActive = false;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f; // Start time
            gameIsPaused = false;
        }
        // If question menu is active, just disable the pause menu.
        else if (QuestionMenu.questionMenuIsActive)
        {
            pauseMenuIsActive = false;
            pauseMenuUI.SetActive(false);
            gameIsPaused = true;
        }
    }

    public void ResumeQuestionMenu()
    {
        pauseMenuIsActive = false;
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuIsActive = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze time
        gameIsPaused = true;
    }

    public void LoadMenu(string sceneName)
    {
        QuestionController.correctCount = 0;
        QuestionController.wrongCount = 0;

        BinaryStormController.moveSpeed = 5f;

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
