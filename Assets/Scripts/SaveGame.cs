using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame //: MonoBehaviour
{
    public float sfxVolume => GetFloat(kSfx);
    public float musicVolume => GetFloat(kMusic);
    public const string kSfx = "SFX Volume";
    public const string kMusic = "Music Volume";
    public string sfx => kSfx;
    public string music => kMusic;

    private float GetFloat(string key)
    {
        if(PlayerPrefs.HasKey(key)) return PlayerPrefs.GetFloat(key);
        else return 0f;
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
