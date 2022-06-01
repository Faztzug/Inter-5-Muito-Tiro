using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private string Teste;
    [SerializeField] private string Tutorial;
    [SerializeField] private string Fase1;
    [SerializeField] private string Fase2;
    [SerializeField] private string Fase3;
    [SerializeField] private Button buttonFase1;
    [SerializeField] private Button buttonFase2;
    [SerializeField] private Button buttonFase3;
    [SerializeField] private Button buttonArena;
    public AudioMixer audioMixer;
    public AudioMixer musicMixer;
    [HideInInspector] public SaveGame save;
    [SerializeField] private GameObject configuracoes;
    [SerializeField] private TMP_Dropdown graphics;
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Slider volume;
    [SerializeField] private Slider music;

    void Awake()
    {
        save = new SaveGame();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        
        LoadVolumeSliders();
        configuracoes?.SetActive(false);
        LockLevels();
    }

    private void LockLevels()
    {
        var progress = save.playerProgress;

        if(progress < 2) buttonFase2.interactable = false;
        if(progress < 3) buttonFase3.interactable = false;
        if(progress < 4)
        {
            buttonArena.interactable = false;
            var text = buttonArena.GetComponentInChildren<TextMeshProUGUI>();
            var cor = text.color;
            cor.r = cor.r - 0.2f;
            cor.g = cor.g - 0.2f;
            cor.b = cor.b - 0.2f;
            cor.a = 0.26f;
            text.color = cor;
        } 
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

    public void AjustarVolumeSFX(float volume)
    {
        save.SaveSFX(volume);
        var halfValue = -20f;
        var multiplier = (volume / 40) * 2f;
        if(multiplier < 0) multiplier = multiplier * -1f;
        if(volume < halfValue) volume = volume * multiplier;
        audioMixer.SetFloat("volume", volume);
    }
    public void AjustarVolumeMusica(float volume)
    {
        save.SaveMusic(volume);
        var halfValue = -20f;
        var multiplier = (volume / 40) * 2f;
        if(multiplier < 0) multiplier = multiplier * -1f;
        if(volume < halfValue) volume = volume * multiplier;
        musicMixer.SetFloat("Volume", volume);
    }
    public void LoadVolumeSliders()
    {
        if(save == null) return;
        AjustarVolumeSFX(save.sfxVolume);
        AjustarVolumeMusica(save.musicVolume);
        Debug.Log("Volume: " + volume);
        Debug.Log("Save: " + save);
        volume.value = save.sfxVolume;
        music.value = save.musicVolume;
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
        Debug.Log("QUIT GAME");
    }
}
