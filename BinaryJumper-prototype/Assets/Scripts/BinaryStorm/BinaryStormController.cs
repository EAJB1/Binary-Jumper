using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BinaryStormController : MonoBehaviour
{
    public Transform Target; // Target is player
    public Transform BinaryStorm;
    public GameObject[] storm1;
    public GameObject[] storm2;
    public GameObject gameOverMenuUI;
    public static float moveSpeed = 5f;
    public float targetSpeed = 30f;
    public float switchMin, switchMax;
    public string storm1Name;
    public string storm2Name;

    void Update()
    {
        if (Target != null)
        {
            // While move speed is less than target value, increment by Time.deltaTime
            if (moveSpeed < targetSpeed)
            {
                moveSpeed += Time.deltaTime;
            }

            // The step size is equal to speed times frame time
            float step = moveSpeed * Time.deltaTime;

            // Move our position a step closer to the target
            transform.position = Vector3.MoveTowards(transform.position, Target.position, step);
        }
        else if (Target == null)
        {
            // Set position of the binary storm to its current position
            transform.position = BinaryStorm.transform.position;
            PlayerController.isDead = true;
            moveSpeed = 5f;
        }
    }
}