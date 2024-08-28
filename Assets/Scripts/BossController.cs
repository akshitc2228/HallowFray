using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    public bool isFlipped = false;

    public void facePlayer()
    {
        //could use flipX from sprite...
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        try
        {
            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f); //flip around y-ais
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        } catch(MissingReferenceException) { }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
