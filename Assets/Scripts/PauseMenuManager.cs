using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private MenuPause menuPause;
    private void Start() 
    {
        if(menuPause == null)
        {
            Debug.LogError("Add Player MenuPause to the Pause Menu on Canvas!");
            menuPause = FindObjectOfType<MenuPause>();
        } 
    }
    public void ResumeGame()
    {
        Debug.Log("Continue");
        menuPause.ResumeGame();
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
