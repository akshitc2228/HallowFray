using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikeBlock : MonoBehaviour
{
    private Rigidbody2D blockBody;
    private bool playerBeneath;

    GameObject playerObject;
    PlayerHealth healthScipt;

    [SerializeField]
    Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        blockBody = GetComponent<Rigidbody2D>();

        playerObject = GameObject.FindGameObjectWithTag("Player");

        if(playerObject != null)
        {
            healthScipt = playerObject.GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (playerPosition != null)
            {
                if (Mathf.Floor(playerPosition.position.x) == Mathf.Floor(transform.position.x - 1))
                {
                    StartCoroutine(triggerFall());
                }
            }
        } catch(NullReferenceException) { }
    }

    IEnumerator triggerFall()
    {
        yield return new WaitForSeconds(0.1f);
        playerBeneath = true;
    }

    private void FixedUpdate()
    {
        if (playerBeneath == true)
        {
            blockBody.gravityScale = 1.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("call kill method now!");
            healthScipt.killPlayer();
            //TODO: Severe memory leaks on killPlayer() end game on this trigger;
        }
    }
}
