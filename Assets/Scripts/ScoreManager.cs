using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public delegate void UpdatePlayerScore(int newScore);

    public static event UpdatePlayerScore OnUpdatePlayerScore;

    private int score = 0;

    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
            }
            return instance;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateScore(int value)
    {
        score += value;
        OnUpdatePlayerScore?.Invoke(score);
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
