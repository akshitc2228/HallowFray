using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelFlag : MonoBehaviour
{
    public string nextSceneName;
    public GameObject gameManager;
    public GameObject pauseMenu;

    public TextUpdater scoreTextScript;
    public Text scoreText;

    private void Awake()
    {
        try
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
        } catch(MissingComponentException) { }
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO: mention which objects to destroy and which to keep:
        //just keep player, background and camera
        //DontDestroyOnLoad(gameManager);
        //DontDestroyOnLoad(pauseMenu);

        scoreTextScript = GameObject.FindGameObjectWithTag("scorecard").GetComponent<TextUpdater>();
        scoreText = scoreTextScript.scoreText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //loading next level every time.
            SceneManager.LoadScene(currentSceneIndex + 1);
            DontDestroyOnLoad(scoreText);
        }
    }
}
