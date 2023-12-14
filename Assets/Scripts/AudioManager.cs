using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Audio[] music;
    public AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlayBGM(string name)
    {
        Audio s = Array.Find(music, x => x.audioName == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            Debug.Log(s.audioName);
            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
    }

    public void BGMVolumn(float value)
    {
        musicSource.volume = value;
    }
}
