using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSawblade : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform leftBarrier; //change to game obj from transform

    [SerializeField]
    private Transform rightBarrier;

    private bool moveLeft, moveRight;

    Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        moveSpeed = 15f;
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightBarrier.position, moveSpeed* Time.deltaTime);
            //transform.Translate(Vector3.right *  moveSpeed * Time.deltaTime);
        } else if (moveLeft)
        {
            //transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, leftBarrier.position, moveSpeed * Time.deltaTime);
        }

        //currentPosition.x = Mathf.Clamp(currentPosition.x, leftBarrier.position.x, rightBarrier.position.x);
        //transform.position = currentPosition;

        if(Vector3.Distance(leftBarrier.position, transform.position) < 0.5 || Vector3.Distance(rightBarrier.position, transform.position) < 0.5)
        {
            moveLeft = !moveLeft;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("LeftBarrier"))
        {
            moveLeft = false;
        }

        if(collision.collider.CompareTag("RightBarrier")) 
        {
            moveLeft = true;
        }
    }
}
