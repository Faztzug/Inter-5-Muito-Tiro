using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
