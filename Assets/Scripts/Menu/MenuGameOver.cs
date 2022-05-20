using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public GameObject gameOver;

    void Start()
    {
        gameOver.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
