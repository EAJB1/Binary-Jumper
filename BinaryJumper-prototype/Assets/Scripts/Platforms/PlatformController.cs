using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject[] platforms; // Prefab list
    public Transform platformContainer;
    public Transform generationPoint;
    public Transform deleationPoint;
    public float yMin, yMax, distanceBetweenMin, distanceBetweenMax;

    private GameObject platformToInstantiate;
    private int index;
    private float platformWidth;
    private float hightGap;
    private float widthGap;

    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            // Choose random platform from array
            index = Random.Range(0, platforms.Length);
            platformToInstantiate = platforms[index];

            // Get the width of the chosen plaform
            platformWidth = platformToInstantiate.transform.localScale.x;

            // Random point on y axis for spawn position
            hightGap = Random.Range(yMin, yMax);
            widthGap = Random.Range(distanceBetweenMin, distanceBetweenMax);

            // Set spawn position
            transform.position = new Vector2(transform.position.x + platformWidth + widthGap, hightGap);

            // Spawn platform as child of 'Platforms'
            GameObject go = Instantiate(platformToInstantiate, transform.position, transform.rotation) as GameObject;
            go.transform.parent = GameObject.Find("Platforms").transform;
        }

        // If a platform is behind the delation point then destroy it
        foreach (Transform child in platformContainer)
        {
            if (child.transform.position.x < deleationPoint.position.x)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
