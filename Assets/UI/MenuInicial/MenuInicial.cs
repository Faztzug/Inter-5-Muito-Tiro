using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private string Teste;
    [SerializeField] private string Tutorial;
    [SerializeField] private string Fase1;
    [SerializeField] private string Fase2;
    [SerializeField] private string Fase3;
    

    public AudioMixer audioMixer;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(Teste);
    }

    public void PlayFase1()
    {
        SceneManager.LoadScene(Fase1);
    }

    public void PlayFase2()
    {
        SceneManager.LoadScene(Fase2);
    }

    public void PlayFase3()
    {
        SceneManager.LoadScene(Fase3);
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
