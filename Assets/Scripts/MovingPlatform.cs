using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private static float minSpeed = 5f;
    
    [SerializeField]
    private static float maxSpeed = 20f;

    //set movement bounds:
    [SerializeField]
    private float upperBound = 33.5f;

    [SerializeField]
    private float lowerBound = -22.7f;

    Vector3 currentPosition;

    //direction
    private int moveDirection;

    private float randomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //allocate these fields once:
        moveDirection = Random.Range(0, 2);
        randomSpeed = Random.Range(minSpeed, maxSpeed);

        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Platform position: " + currentPosition.y + ", Move direction: " + moveDirection);

        // Move up first
        if (moveDirection == 0)
        {
            currentPosition.y += randomSpeed * Time.deltaTime;
            if (currentPosition.y >= upperBound)
            {
                currentPosition.y = upperBound;
                /*Debug.Log("Reached the top. Current position : " + currentPosition.y + "Upper bound: " + upperBound);*/
                moveDirection = 1; // Change direction to move down
            }
        }

        // Move down first
        if (moveDirection == 1)
        {
            currentPosition.y -= randomSpeed * Time.deltaTime;
            if (currentPosition.y <= lowerBound)
            {
                currentPosition.y = lowerBound;
                /*Debug.Log("Reached the bottom. Current position : " + currentPosition.y + "Lower bound: " + lowerBound);*/
                moveDirection = 0; // Change direction to move up
            }
        }

        // Update the platform's position
        transform.position = currentPosition;
    }
}
