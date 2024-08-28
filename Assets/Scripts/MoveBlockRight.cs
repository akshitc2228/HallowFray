using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlockRight : MonoBehaviour
{
    [SerializeField]
    private Transform otherWall;

    [SerializeField]
    private Transform leftBarrier;

    private bool hitOtherWall;

    [SerializeField]private float openingSpeed;
    [SerializeField]private float closingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        hitOtherWall = false;

        openingSpeed = 9f;
        closingSpeed = 45f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!hitOtherWall)
        {
            transform.Translate(Vector3.right * closingSpeed * Time.deltaTime);
        }

        if(hitOtherWall)
        {
            transform.Translate(Vector3.left * openingSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("right_block"))
        {
            hitOtherWall = true;
        }
        
        if (collision.collider.CompareTag("leftSmashLimit"))
        {
            hitOtherWall = false;
        }
    }
}
