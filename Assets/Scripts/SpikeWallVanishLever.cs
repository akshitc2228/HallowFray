using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWallVanishLever : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(GameObject.FindGameObjectWithTag("vanishSpikeWall1"));
            Destroy(GameObject.FindGameObjectWithTag("vanishSpikeWall2"));
        }
    }
}
