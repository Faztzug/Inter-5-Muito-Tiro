using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] AudioSource thisMusic;
    [SerializeField] AudioMixer musicMixer;
    SaveGame save;
    // Start is called before the first frame update
    void Start()
    {
        save = new SaveGame();

        AjustarVolumeMusica(save.musicVolume);


        foreach (var another in GameObject.FindGameObjectsWithTag("Music"))
        {
            if(another == this.gameObject)
            {
                Debug.Log("MUSIC FOUND HIMSEELF");
                continue;
            } 

            if(another.TryGetComponent<AudioSource>(out AudioSource oldMusic))
            {
                if(oldMusic.clip == thisMusic.clip)
                {
                    Destroy(this.gameObject);
                    return;
                }
                else
                {
                    Destroy(oldMusic.gameObject);
                } 
            }
        
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
    public void AjustarVolumeMusica(float volume)
    {
        var halfValue = -20f;
        var multiplier = (volume / 40) * 2f;
        if(multiplier < 0) multiplier = multiplier * -1f;
        if(volume < halfValue) volume = volume * multiplier;
        musicMixer.SetFloat("Volume", volume);
    }
}
