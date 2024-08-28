using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByGravity : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    PlayerHealth healthScript;

    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            healthScript = player.GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player fell to their death");
            healthScript.killPlayer();
        }
    }
}
