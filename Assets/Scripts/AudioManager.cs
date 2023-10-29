using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musics, sfx;
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    private int _sfxSourcesIndex;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayMusic(string musicName, bool loop = false)
    {
        var s = Array.Find(musics, x => x.name == musicName);

        if (s == null) return;
        if (s.clip == musicSource.clip) return;
        
        musicSource.clip = s.clip;
        musicSource.Play();
        musicSource.loop = loop;
    }
    
    public void StopMusic(string musicName)
    {
        var s = Array.Find(musics, x => x.name == musicName);

        if (s == null) return;
        if (s.clip != musicSource.clip) return;
        musicSource.Stop();
    }

    public void PlaySFX(string musicName)
    {
        var s = Array.Find(sfx, x => x.name == musicName);

        if (s == null) return;
        sfxSources[_sfxSourcesIndex].PlayOneShot(s.clip);
        _sfxSourcesIndex = (_sfxSourcesIndex + 1) % sfxSources.Length;

    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    
    public void ToggleSFX()
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.mute = !sfxSource.mute;
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    
    public void SetSFXVolume(float volume)
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }
    }
}