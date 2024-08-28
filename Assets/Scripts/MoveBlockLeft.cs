using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlockLeft : MonoBehaviour
{
    [SerializeField]
    private Transform otherWall;

    [SerializeField]
    private Transform rightBarrier;

    private bool hitOtherWall;

    [SerializeField] private float openingSpeed;
    [SerializeField] private float closingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        hitOtherWall = false;

        openingSpeed = 11f;
        closingSpeed = 45f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hitOtherWall)
        {
            transform.Translate(Vector3.left * closingSpeed * Time.deltaTime);
        }

        if (hitOtherWall)
        {
            transform.Translate(Vector3.right * openingSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("left_block"))
        {
            hitOtherWall = true;
        }
        
        if (collision.collider.CompareTag("rightSmashLimit"))
        {
            hitOtherWall = false;
        }
    }
}
