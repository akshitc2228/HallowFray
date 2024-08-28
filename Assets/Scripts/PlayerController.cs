using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;

    [SerializeField]
    private float jumpForce = 45f;

    private float movementX;
    private float movementY;

    //animator variables:
    private string run = "Run";
    private string grounded = "isGrounded";
    private string verticalSpeed = "verticalSpeed";
    private string attacking = "Attacking";

    public bool isGrounded = true;
    public bool axeAcquired = false;

    //game tags:
    public string GROUND = "Ground";

    //game object components:
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerCollider;
    private SpriteRenderer playerSR;
    private Animator playerAnimator;
    
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //subscribing to the event:
        AxePowerUp.OnPowerUpCollected += handleAxePowerUp;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        animatePlayer();
        //Debug.Log(playerAnimator.GetBool(attacking));
        //sDebug.Log(axeAcquired);
    }

    void LateUpdate()
    {
        playerJump();
    }

    void movePlayer() 
    { 
        movementX = UnityEngine.Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * speed * Time.deltaTime;
    }

    void animatePlayer()
    {
        if (isGrounded)
        {
            if (movementX > 0)
            {
                playerAnimator.SetBool(run, true);
                playerSR.flipX = false;
            }
            else if (movementX < 0)
            {
                playerAnimator.SetBool(run, true);
                playerSR.flipX = true;
            }
            else
            {
                playerAnimator.SetBool(run, false);
            }

            if(axeAcquired && UnityEngine.Input.GetKeyDown(KeyCode.J))
            {
                playerAnimator.SetBool("axeObtained", true);
                playerAnimator.SetBool(attacking, true);
            }
        }
        else
        {
            playerAnimator.SetBool(grounded, false);
            playerAnimator.SetFloat(verticalSpeed, playerRigidbody.velocity.y);
        }
    }

    void playerJump()
    {
        if(UnityEngine.Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(GROUND))
        {
            isGrounded = true;
            playerAnimator.SetBool(grounded, true);
        }
    }

    private void handleAxePowerUp()
    {
        //TODO: logic and animation code
        axeAcquired = true;
    }

    private void OnDestroy()
    {
        AxePowerUp.OnPowerUpCollected -= handleAxePowerUp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("slumDoorKey"))
        {
            Destroy(GameObject.FindWithTag("slumDoorKey"));
            Destroy(GameObject.FindWithTag("exitWall"));
        }
    }
}
