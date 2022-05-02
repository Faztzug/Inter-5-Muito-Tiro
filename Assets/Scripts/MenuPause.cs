using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    /*public void LoadMenu() Caso seja feito um menu de pause
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }*/

    public void Options()  
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
