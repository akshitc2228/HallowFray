using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AxePowerUp : MonoBehaviour
{

    //creating a delegate and an event
    public delegate void AxePowerUpEvent();
    public static event AxePowerUpEvent OnPowerUpCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) {
            OnPowerUpCollected?.Invoke();
            //destory the power-up
            Destroy(GameObject.FindWithTag("AxeObject"));
            UnityEngine.Debug.Log("Axe obj destroyed");
        }
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
