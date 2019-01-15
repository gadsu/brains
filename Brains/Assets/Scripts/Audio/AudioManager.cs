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
        {
            instance = this;
            _sceneAudio.InitSounds(gameObject);
        }
        else
        {
            instance.SetActiveSceneAudio(_sceneAudio);
            Destroy(gameObject);
            return;
        }

        instance.SetActiveSceneAudio(_sceneAudio);

        DontDestroyOnLoad(gameObject);
    }

    public void Start ()
    {
        /*_sceneAudio.Play("Background Music");
        _sceneAudio.Play("Background Music High");
        _sceneAudio.SetVolume("Background Music High", 0f);
        //SetActiveSceneAudio(SceneManager.GetActiveScene().name);*/
    }

    public void SetActiveSceneAudio(ObjectSounds p_sceneAudio)
    {
        for (int i = 0; i < p_sceneAudio._objectSounds.Capacity; i++)
        {
            p_sceneAudio.Play(p_sceneAudio._objectSounds[i].name, p_sceneAudio._objectSounds[i].defaultVolume * p_sceneAudio._objectSounds[i].volumeScale);
        }
        /*switch (p_SceneName)
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
        }*/
    }

    public void Play(string name)
    {
        if (PauseMenu.GamePaused) _sceneAudio.SetPitch(name, _sceneAudio.GetPitch(name) * 3f);

        _sceneAudio.Play(name);
    }

    public void SetVol(string name, float vol)
    {
        vol = Mathf.Clamp(vol, 0, 1f);
        _sceneAudio.SetVolume(name, vol);
    }

    public Sound GetSound(string name)
    {
        return _sceneAudio.GetSound(name);
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