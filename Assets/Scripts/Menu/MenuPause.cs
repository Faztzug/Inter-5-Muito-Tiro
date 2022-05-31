using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuPause : MonoBehaviour
{
    public GameObject player;
    public GameObject[] pauseMenu;
    public GameObject[] pauseMenuOnlyDeactive;
    public AudioMixer audioMixer;
    public AudioMixer musicMixer;
    public GameState state;
    private SpeedState timeState;

    void Awake()
    {
        if(player == null) Debug.LogError("COLOCAR PLAYER NO CANVAS PARA O PAUSE FUNCIONAR");
        state = player.GetComponent<GameState>();
    }
    void Start()
    {
        SetPauseGO(false);
        LoadGameSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (state.TimeS == SpeedState.Paused)
            {
                Debug.Log("PAUSE RESUME");
                Cursor.lockState = CursorLockMode.Locked;
                ResumeGame();
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
        SetPauseGO(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Debug.Log("RESUME GAME");
        StartCoroutine(MakeSureLockCursor());
        Cursor.lockState = CursorLockMode.Locked;
        state.TimeS = SpeedState.Running;
        SetPauseGO(false);
        Time.timeScale = 1f;
    }
    IEnumerator MakeSureLockCursor()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetPauseGO(bool value)
    {
        foreach (var item in pauseMenu)
        {
            item.SetActive(value);
        }

        if(value == false)
        {
            foreach (var item in pauseMenuOnlyDeactive)
            {
                item.SetActive(value);
            }
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    private void LoadGameSettings()
    {
        if(PlayerPrefs.HasKey(state.save.sfx)) AjustarVolume(PlayerPrefs.GetFloat(state.save.sfx));
        if(PlayerPrefs.HasKey(state.save.music)) AjustarVolumeMusica(PlayerPrefs.GetFloat(state.save.music));
    }

    public void AjustarVolume(float volume)
    {
        state.save.SaveSFX(volume);
        var halfValue = -20f;
        var multiplier = (volume / 40) * 2f;
        if(multiplier < 0) multiplier = multiplier * -1f;
        if(volume < halfValue) volume = volume * multiplier;
        audioMixer.SetFloat("volume", volume);
    }
    public void AjustarVolumeMusica(float volume)
    {
        state.save.SaveMusic(volume);
        var halfValue = -20f;
        var multiplier = (volume / 40) * 2f;
        if(multiplier < 0) multiplier = multiplier * -1f;
        if(volume < halfValue) volume = volume * multiplier;
        musicMixer.SetFloat("Volume", volume);
    }
    
    public void AjustarSensibilidade(float value)
    {
        state.MouseSensibility = value;
    }
    public void AjustarAceleracao(float value)
    {
        var maxValue = 5f;
        var minValue = 0.1f;
        var reverseValue = maxValue - value;
        if(reverseValue < minValue) reverseValue = minValue;

        state.MouseAccelaration = reverseValue;
        Debug.Log("AJUSTAR ACCEL " + reverseValue);
    }

    public void AjustarQualidade(int numeroQualidade)
    {
        QualitySettings.SetQualityLevel(numeroQualidade);
    }

    public void TelaCheia(bool estaCheia)
    {
        Screen.fullScreen = estaCheia;
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
