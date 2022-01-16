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
            // If pause menu is active then on next key down, resume game
            if (gameIsPaused && pauseMenuIsActive)
            {
                Resume();
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
        pauseMenuIsActive = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Start time
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

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
