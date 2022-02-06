using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionMenu : MonoBehaviour
{
    public GameObject questionMenuUI;
    public GameObject transparentMenuUI;
    public static bool questionMenuIsActive = false;
    public static bool transparentMenuIsActive = false;

    void Update()
    {
        // If key press 'Enter'
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // If game is paused, Pause menu or Game Over menu are not active, then resume game.
            if (PauseMenu.gameIsPaused && QuestionController.canResume && questionMenuIsActive && !PauseMenu.pauseMenuIsActive)
            {
                Resume();
                StartCoroutine(Wait());
            }
            // If game is not paused, pause menu or gameover menu is not active, then pause game.
            else if (!PauseMenu.gameIsPaused && !PauseMenu.pauseMenuIsActive || !PauseMenu.gameIsPaused && !GameOverMenu.gameOverMenuIsActive)
            {
                Pause();
            }
        }
    }

    IEnumerator Wait()
    {
        // Transparent menu enabled
        transparentMenuUI.SetActive(true);
        transparentMenuIsActive = true;

        yield return new WaitForSeconds(1);

        // Transparent menu disabled
        transparentMenuUI.SetActive(false);
        transparentMenuIsActive = false;
    }

    public void Resume()
    {
        questionMenuIsActive = false;
        questionMenuUI.SetActive(false);
        Time.timeScale = 1f; // Start time
        PauseMenu.gameIsPaused = false;

        // Transparent menu disabled
        transparentMenuUI.SetActive(false);
        transparentMenuIsActive = false;
    }

    void Pause()
    {
        // Show question menu
        questionMenuIsActive = true;
        questionMenuUI.SetActive(true);
        
        // Freeze time
        Time.timeScale = 0f;
        PauseMenu.gameIsPaused = true;

        // Allow answer generation
        QuestionController.canGenerateQuestion = true;
    }
}
