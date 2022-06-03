using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame //: MonoBehaviour
{
    public float sfxVolume => GetFloat(kSfx);
    public float musicVolume => GetFloat(kMusic);
    public int playerProgress => GetInt(kProgress);
    public const string kSfx = "SFX Volume";
    public const string kMusic = "Music Volume";
    public const string kProgress = "Progress";
    public string sfx => kSfx;
    public string music => kMusic;

    private float GetFloat(string key)
    {
        if(PlayerPrefs.HasKey(key)) return PlayerPrefs.GetFloat(key);
        else return 0f;
    }
    private int GetInt(string key)
    {
        if(PlayerPrefs.HasKey(key)) return PlayerPrefs.GetInt(key);
        else return 0;
    }

    public void SaveProgress(int value)
    {
        if(GetInt(kProgress) >= value) return;
        PlayerPrefs.SetInt(kProgress, value);
    }
    public void CleanProgress()
    {
        PlayerPrefs.SetInt(kProgress, 0);
    }

    public void SaveSFX(float value)
    {
        PlayerPrefs.SetFloat(kSfx, value);
    }
    public void SaveMusic(float value)
    {
        PlayerPrefs.SetFloat(kMusic, value);
    }
}
