using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject controls;
    public GameObject showControls;
    public GameObject startGame;
    public GameObject title;
    public GameObject quitGame;

    private void Start()
    {
        controls.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            controls.SetActive(false);

            showControls.SetActive(true);
            startGame.SetActive(true);
            title.SetActive(true);
            quitGame.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Intro screen");
    }

    public void ShowControls()
    {
        controls.SetActive(true);

        showControls.SetActive(false);
        startGame.SetActive(false);
        title.SetActive(false);
        quitGame.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
