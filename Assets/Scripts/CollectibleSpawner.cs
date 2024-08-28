using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject spawnCandy;
    public Transform[] spawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform spawnPoint in spawnPositions)
        {
            Instantiate(spawnCandy, spawnPoint.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
