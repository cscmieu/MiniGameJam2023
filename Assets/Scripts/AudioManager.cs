using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musics, sfx;
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    private int _sfxSourcesIndex = 0;
    
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

    private void Start()
    {
        //PlayMusic("Theme")
    }
    
    public void PlayMusic(string name, bool loop = false)
    {
        Sound s = Array.Find(musics, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else
        {
            if(s.clip != musicSource.clip)
            {
                musicSource.clip = s.clip;
                musicSource.Play();
                musicSource.loop = loop;
            }
        }
    }
    
    public void StopMusic(string name)
    {
        Sound s = Array.Find(musics, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else
        {
            if(s.clip == musicSource.clip)
            {
                musicSource.Stop();
            }
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX not found");
        }

        else
        {
            sfxSources[_sfxSourcesIndex].PlayOneShot(s.clip);

            _sfxSourcesIndex = (_sfxSourcesIndex + 1) % sfxSources.Length;
        }

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