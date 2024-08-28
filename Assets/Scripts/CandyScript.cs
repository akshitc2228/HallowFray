using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandyScript : MonoBehaviour
{

    public int scoreValue = 10;
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
            ScoreManager.Instance.UpdateScore(scoreValue);
            gameObject.SetActive(false);
        }
    }
}
