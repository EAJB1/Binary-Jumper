using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public static bool isEasy = false;
    public static bool isMedium = false;
    public static bool isHard = false;
    public static bool canGenerateQuestion = false;
    public static bool answerIsWrong = false;
    public static bool canResume = false;
    public static int correctCount = 0;
    public static int wrongCount = 0;

    [Header("Bools")]
    public bool questionIsGenerated = false;
    public bool answerIsChecked = false;
    public bool canCheckAnswer = false;
    public bool answerIsCorrect = false;
    public bool formatExceptionError = false;

    [Header("Game Objects")]
    public GameObject questionMenuUI;
    public GameObject questionTxtObj;
    public GameObject inputFieldObj;
    public GameObject correctObj;
    public GameObject wrongObj;
    public GameObject[] invalidInputText;
    public GameObject currentInvalidInputText;

    public Text questionTxt;
    public InputField inputField;

    private string userInputStr = null;
    public string questionStr = null;
    private string correctAnswerStr = null;
    private string correctAnswer = null;
    
    private int charLength = 0;
    private int userInputInt = 0;
    private int correctAnswerInt = 0;
    private int indexOfMessages;

    void Start()
    {
        // Find question object in scene and assign the text object to a variable
        questionTxtObj = GameObject.Find("Question");
        questionTxt = questionTxtObj.GetComponent<Text>();
    }

    void Update()
    {
        // If return key is down, game is paused, question menu is active, and all other menus are not active.
        if (PauseMenu.gameIsPaused && QuestionMenu.questionMenuIsActive && !PauseMenu.pauseMenuIsActive
            || !GameOverMenu.gameOverMenuIsActive && !QuestionMenu.transparentMenuIsActive)
        {
            // If ready then generate question
            if (!questionIsGenerated && !answerIsChecked && canGenerateQuestion && !canCheckAnswer)
            {
                // Clear input field
                inputField.text = null;

                GenerateQuestion();

                // Activate input field
                inputField.ActivateInputField();
                inputField.GetComponent<CanvasGroup>().interactable = true;
                inputField.Select();

                // Change bool values
                questionIsGenerated = true;
                canGenerateQuestion = false;
                canCheckAnswer = true;
                canResume = false;
            }
            
            // If ready then check answer
            if (Input.GetKeyDown(KeyCode.Return) && questionIsGenerated && !answerIsChecked && !canGenerateQuestion && canCheckAnswer)
            {
                // Activate input field
                inputField.ActivateInputField();
                inputField.GetComponent<CanvasGroup>().interactable = true;
                inputField.Select();

                // Disable any messages
                foreach (GameObject msg in invalidInputText)
                {
                    msg.SetActive(false);
                }

                InputHandling();

                // If there is a format exception error then set bools to check the answer again
                if (formatExceptionError)
                {
                    questionIsGenerated = true;
                    answerIsChecked = false;
                    canCheckAnswer = true;
                }
                // If there is no error then check the answer and set bools to be ready to resume the game
                else if (!formatExceptionError)
                {
                    CheckAnswer();

                    questionIsGenerated = false;
                    answerIsChecked = true;
                    canCheckAnswer = false;
                }
            }

            // If ready then resume
            if (Input.GetKeyDown(KeyCode.Return) && !questionIsGenerated && answerIsChecked && !canGenerateQuestion && !canCheckAnswer 
                && !canResume)
            {
                if (answerIsCorrect || answerIsWrong)
                {
                    // Deactivate input field if it's still actives
                    inputField.DeactivateInputField();
                    inputField.GetComponent<CanvasGroup>().interactable = false;

                    // Change bool values
                    answerIsChecked = false;
                    canResume = true;
                }
            }
        }
    }

    // Saves the user input as the variable 'input'
    public void ReadStringInput(string s)
    {
        userInputStr = s;
    }

    public void GenerateQuestion()
    {
        // Reset 'questionStr', 'correctObj' and 'wrongObj'
        questionStr = null;
        correctObj.SetActive(false);
        wrongObj.SetActive(false);

        // Set question number length depending on the difficulty level
        if (isEasy)
        {
            charLength = 3; // Range of 4
        }
        else if (isMedium)
        {
            charLength = 5; // Range of 6
        }
        else if (isHard)
        {
            charLength = 7; // Range of 8
        }

        // Generate random binary number with specified character length
        for (int i = 0; i <= charLength; i++)
        {
            int randomNum = UnityEngine.Random.Range(0, 2); // Range of 0 to 1
            questionStr += randomNum.ToString();
        }

        // Assign string to text field
        questionTxt.text = questionStr;
    }

    public void InputHandling()
    {
        // Reset bool before the check
        formatExceptionError = false;

        // Look for format exception error, mainly for null input.
        try
        {
            // Convert input from string to int
            userInputInt = System.Convert.ToInt32(userInputStr);
        }
        // If there is an error, display a helpful message
        catch (FormatException)
        {
            formatExceptionError = true;

            // Choose random object from array
            indexOfMessages = UnityEngine.Random.Range(0, invalidInputText.Length);
            currentInvalidInputText = invalidInputText[indexOfMessages];

            // Set object to active
            currentInvalidInputText.SetActive(true);
        }
    }

    public void CheckAnswer()
    {
        // Reset 'correct' and 'wrong' before checking the answer
        answerIsCorrect = false;
        answerIsWrong = false;

        // Convert int to binary
        correctAnswerStr = System.Convert.ToString(userInputInt, 2);

        // Convert to int
        correctAnswerInt = System.Convert.ToInt32(string.Format("{0}", correctAnswerStr));

        // Conveert binary to string (to add zeros that the int doesn't show)
        if (isEasy)
        {
            correctAnswer = correctAnswerInt.ToString("D4");
        }
        else if (isMedium)
        {
            correctAnswer = correctAnswerInt.ToString("D6");
        }
        else if (isHard)
        {
            correctAnswer = correctAnswerInt.ToString("D8");
        }

        // Compare questionStr to correctAnswer:

        // If answer is correct
        if (questionStr == correctAnswer)
        {
            answerIsCorrect = true;
            answerIsWrong = false;

            // Enable 'correctObj' and disable 'wrongObj'
            correctObj.SetActive(true);
            wrongObj.SetActive(false);

            // Decrease storm speed
            BinaryStormController.moveSpeed -= 5f;

            // Increment correct count
            correctCount += 1;
        }
        // If answer is wrong
        else if (questionStr != correctAnswer)
        {
            answerIsWrong = true;
            answerIsCorrect = false;

            // Enable 'wrongObj' and disable 'correctObj'
            wrongObj.SetActive(true);
            correctObj.SetActive(false);

            // Increase storm speed
            BinaryStormController.moveSpeed += 2.5f;

            // Increment wrong count
            wrongCount += 1;
        }
    }

    // Increment correct or wrong question counter score. Resume game on 'Return' key down.
    public void Resume()
    {
        // Disable question menu and resume game
        QuestionMenu.questionMenuIsActive = false;
        questionMenuUI.SetActive(false);
        Time.timeScale = 1f; // Start time
        PauseMenu.gameIsPaused = false;
    }
}
