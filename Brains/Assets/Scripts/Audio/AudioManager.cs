using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public ObjectSounds _sceneAudio;

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

        _sceneAudio.LoadDictionary(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Start ()
    {
        _sceneAudio.Play("Background Music");
        _sceneAudio.Play("Background Music High");
        _sceneAudio.SetVolume("Background Music High", 0f);
        SetActiveSceneAudio(SceneManager.GetActiveScene().name);
    }

    public void SetActiveSceneAudio(string p_SceneName)
    {
        switch (p_SceneName)
        {
            case "Menu":
                _sceneAudio.SetVolume("Main Music", 1f);
                _sceneAudio.SetVolume("Background Music", 0f);
                _sceneAudio.SetVolume("Background Music High", 0f);
                break;
            default:
                _sceneAudio.SetVolume("Main Music", 0f);
                _sceneAudio.SetVolume("Background Music", 1f);
                break;
        }
    }

    public void Play(string name)
    {
        Sound s = new Sound();
        if (_sceneAudio._dictSounds.TryGetValue(name, out s))
        {
            if (PauseMenu.GamePaused)
            {
                s.source.pitch *= 3;
            }

            s.source.Play();
        }
    }

    public void SetVol(string name, float vol)
    {
        vol = Mathf.Clamp(vol, 0, 1f);
        Sound s = new Sound();
        if (_sceneAudio._dictSounds.TryGetValue(name, out s))
        {
            s.source.volume = vol;
        }
    }

    public Sound GetSound(string name)
    {
        Sound s = new Sound();
        if (_sceneAudio._dictSounds.TryGetValue(name, out s))
        {
            return s;
        }

        return null;
    }
}

/*if (s == null)
//{
//    Debug.LogWarning("Sound: " + name + " not found!");

//    if (PauseMenu.GamePaused)
//    {
//        s.source.pitch *= 3f;
//    }
//    return;
//}
s.source.Play();*/

//Sound s = Array.Find(sounds, Sound => Sound.name == name);
//if (s == null)
//{
//    Debug.LogWarning("Sound: " + name + " not found!");
//}
//s.source.volume = vol;