using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EpilogueText : MonoBehaviour
{
    public float textMoveSpeed = 0.05f;

    public Transform upperBound;

    private Vector3 startPosition;
    private Vector3 stoppingPosition;

    private bool hasReachedStoppingPosition = false;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        if (upperBound != null)
        {
            stoppingPosition = upperBound.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += 0.10f;
        if (!hasReachedStoppingPosition)
        {
            transform.position = Vector3.Lerp(startPosition, stoppingPosition, elapsedTime / Vector3.Distance(startPosition, stoppingPosition));

            if (transform.position.y >= stoppingPosition.y)
            {
                hasReachedStoppingPosition = true;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
