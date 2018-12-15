﻿using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

	// Use this for initialization
	void Awake () {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
        {
            //Use this function to play a certain sound in script
            //FindObectOfType<AudioManager>().Play("name");
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

	}

    public void Start ()
    {
        Play("BGMusic");
        Play("BGMusicHigh");
        setVol("BGMusicHigh", 0);
    }

    public void Update()
    {
        SetActiveSceneAudio(SceneManager.GetActiveScene().name);
    }

    public void SetActiveSceneAudio(string p_SceneName)
    {
        switch (p_SceneName)
        {
            case "Menu":
                setVol("MMusic", 0.2f);
                setVol("BGMusic", 0f);
                setVol("BGMusicHigh", 0f);
                break;
            default:
                setVol("MMusic", 0f);
                setVol("BGMusic", 0.1f);
                break;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");

            if (PauseMenu.GamePaused)
            {
                s.source.pitch *= 3f;
            }
            return;
        }
        s.source.Play();

    }
    public void setVol(string name, float vol)
    {
        Mathf.Clamp(vol, 0, 1f);
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        s.source.volume = vol;
    }

    public Sound getSound(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        return s;
    }
}
