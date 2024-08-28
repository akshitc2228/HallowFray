using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveIntroText : MonoBehaviour
{
    public float textMoveSpeed = 1.5f;

    public Text enterPrompt;

    public Transform upperBound;

    private Vector3 startPosition;
    private Vector3 stoppingPosition;

    private bool hasReachedStoppingPosition = false;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        enterPrompt.enabled = false;
        startPosition = transform.position;
        if(upperBound != null )
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
            transform.position = Vector3.Lerp(startPosition, stoppingPosition, elapsedTime /Vector3.Distance(startPosition, stoppingPosition));

            if (transform.position.y >= stoppingPosition.y)
            {
                hasReachedStoppingPosition = true;
                Debug.Log("Press Enter appears now"); //add enter button prompt
                enterPrompt.enabled = true;
            }
        }
    }
}
