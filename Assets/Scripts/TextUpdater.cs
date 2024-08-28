using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public Text scoreText;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            scoreText = GetComponent<Text>();
        }
        catch (MissingReferenceException) { }

        ScoreManager.OnUpdatePlayerScore += UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
