using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    private void Awake()
    {
        try
        {
            pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu");
        } catch (MissingReferenceException) { }
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                resumeGame();
            } else
            {
                pauseGame();
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked on pause menu");
            //Debug.Log("");
        }
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame()
    {
        Debug.Log("in resume method");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Debug.Log("go to main menu now");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

    public void quitGame()
    {
        Debug.Log("in quit method");
        Application.Quit();
    }
}
