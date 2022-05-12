using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuInicial : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayFase1()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayFase2()
    {
        SceneManager.LoadScene(3);
    }

    public void AjustarVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void AjustarQualidade(int numeroQualidade)
    {
        QualitySettings.SetQualityLevel(numeroQualidade);
    }

    public void TelaCheia(bool estaCheia)
    {
        Screen.fullScreen = estaCheia;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
