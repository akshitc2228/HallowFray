using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 20f;
    public bool canClimb;
    public bool isClimbing; //for physics in fixedUpdate

    //animator variables:
    private string climbAnimation = "canClimb";

    [SerializeField]
    private Rigidbody2D playerRigidBody;

    [SerializeField]
    private Animator playerAnimator;

    //to access player info:
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            // Get the player script component
            playerScript = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            UnityEngine.Debug.LogError("Player object not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canClimb)
        {
            playerClimb();
        }
        /*animateClimbing();*/
    }

    void playerClimb()
    {
        vertical = Input.GetAxis("Vertical");

        if (canClimb && vertical > 0)
        {
            isClimbing = true;
        }

        if(canClimb && vertical < 0 && !playerScript.isGrounded)
        {
            isClimbing = true;
        } 
    }

    private void FixedUpdate()
    {
        //TODO: disable gravity and move the player:
        if (canClimb && isClimbing)
        {
            playerRigidBody.gravityScale = 0f;

            playerRigidBody.velocity = new Vector2 (0, vertical * speed);
            playerRigidBody.constraints |= RigidbodyConstraints2D.FreezePositionX; //freeze X movement
        }
        else
        {
            playerRigidBody.gravityScale = 1f;
            playerRigidBody.constraints &= ~RigidbodyConstraints2D.FreezePositionX; //unfreeze X movement
        }
    }

    void animateClimbing()
    {
        if(isClimbing)
        {
            playerAnimator.SetBool(climbAnimation, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            UnityEngine.Debug.Log("left ladder");
            canClimb = false;
        }
    }
}
