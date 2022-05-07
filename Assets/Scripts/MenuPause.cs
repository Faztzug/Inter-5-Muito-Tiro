using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseMenu;
    private GameState state;
    private SpeedState timeState;

    void Start()
    {
        if(player == null) Debug.LogError("COLOCAR PLAYER NO CANVAS PARA O PAUSE FUNCIONAR");
        state = player.GetComponent<GameState>();
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (state.TimeS == SpeedState.Paused)
            {
                ResumeGame();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                PauseGame();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void PauseGame()
    {
        state.TimeS = SpeedState.Paused;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        state.TimeS = SpeedState.Running;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Options()  
    {
        
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene(0);
    }
}
