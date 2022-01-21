using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryStormSpeed : MonoBehaviour
{
    GameObject speedObj;
    public static Text speedTxt;
    public int maxLength;

    private float current;
    private int currentInt;
    private string currentStr;

    void Start()
    {
        speedObj = GameObject.Find("BinaryStormSpeed");
        speedTxt = speedObj.GetComponent<Text>();

        speedTxt.text = "0";
    }

    void Update()
    {
        if (GameOverMenu.gameOverMenuIsActive)
        {
            speedTxt.text = "0";
        }
        else if (!GameOverMenu.gameOverMenuIsActive)
        {
            // Get moveSpeed
            current = BinaryStormController.moveSpeed;
            currentStr = current.ToString();
            
            //currentStr = BinaryStormController.moveSpeed.ToString();

            speedTxt.text = currentStr;

            //speedStr = currentStr;
            //speedTxt.text = speedStr;
        }
    }
    /*
    private void OnGUI()
    {
        speedStr = GUI.TextField(new Rect (180, 0, 0, 0), speedStr, maxLength);
    }*/
}