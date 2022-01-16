using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuUI;
    public AudioMixer musicMixer;
    public AudioMixer audioMixer;

    void Start()
    {
        Cursor.visible = true;

        // If main menu is not active, activate it
        if (!mainMenuUI.activeSelf)
        {
            mainMenuUI.SetActive(true);
        }
    }

    public void PlayGame()
    {
        // Load next scene in build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        Cursor.visible = false;
    }

    public void Easy()
    {
        QuestionController.isEasy = true;
        QuestionController.isMedium = false;
        QuestionController.isHard = false;
    }

    public void Medium()
    {
        QuestionController.isEasy = false;
        QuestionController.isMedium = true;
        QuestionController.isHard = false;
    }

    public void Hard()
    {
        QuestionController.isEasy = false;
        QuestionController.isMedium = false;
        QuestionController.isHard = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SetVolume(float musicVolume)
    {
        musicMixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetSFXVolume(float sfxVolume)
    {
        audioMixer.SetFloat("sfxVolume", sfxVolume);
    }
}
