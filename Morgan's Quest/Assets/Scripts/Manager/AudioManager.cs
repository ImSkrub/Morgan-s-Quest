using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("----Audio Source-----")]
    [SerializeField] private AudioSource MusicSource, SFXSource;
    [Header("-----Audio Clip-----")]
    public Sound[] MusicSounds, SFXSounds;
    private int lastLevel = -1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    private void Update()
    {
        int currentLevel = LevelManager.instance?.currentLevel ?? -1;

        if (currentLevel != lastLevel)
        {
            lastLevel = currentLevel;

            switch (currentLevel)
            {
                case 0:
                    PlayMenuMusic();
                    break;
                case 3:
                    PlayLoseMusic();
                    break;
                case 4:
                    PlayWinMusic();
                    break;
                default:
                    Debug.Log($"No specific music for level {currentLevel}");
                    break;
            }
        }
    }

    public void PlayMusic(string name)
    {
        if (MusicSource == null)
        {
            Debug.LogWarning("MusicSource is not assigned!");
            return;
        }

        Sound s = Array.Find(MusicSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            MusicSource.clip = s.Clip;
            MusicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (MusicSource == null)
        {
            Debug.LogWarning("MusicSource is not assigned!");
            return;
        }

        MusicSource.Stop();
    }
    public void PlaySFX(string name)
    {
        if (SFXSource == null)
        {
            Debug.LogWarning("SFXSource is not assigned!");
            return;
        }

        Sound s = Array.Find(SFXSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.LogWarning($"SFX '{name}' not found in SFXSounds array");
            return;
        }

        if (s.Clip == null)
        {
            Debug.LogWarning($"SFX '{name}' has no assigned audio clip!");
            return;
        }

        Debug.Log($"Playing SFX: {name}");
        SFXSource.PlayOneShot(s.Clip);
    }

    public void PlayMenuMusic()
    {
        StopMusic();
        PlayMusic("BG menu"); // Replace with your actual menu music name
    }

    public void PlayWinMusic()
    {
        StopMusic();
        PlayMusic("Win"); // Replace with your actual win music name
    }

    public void PlayLoseMusic()
    {
        StopMusic();
        PlayMusic("Lose"); // Replace with your actual lose music name
    }

    public void MusicVolume(float volume)
    {
        if (MusicSource != null)
        {
            MusicSource.volume = volume;
        }
    }

    public void SFXVolume(float volume)
    {
        if (SFXSource != null)
        {
            SFXSource.volume = volume;
        }
    }
}
