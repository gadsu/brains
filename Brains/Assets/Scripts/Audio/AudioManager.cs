using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public ObjectSounds _sceneAudio;

    //public static AudioManager instance;

	// Use this for initialization
	void Awake () {

        /*if (instance == null)
        {
            instance = this;
            _sceneAudio.InitSounds(gameObject);
        }
        else
        {
            instance.SetActiveSceneAudio(_sceneAudio);
            Destroy(this);
            return;
        }*/

        _sceneAudio.InitSounds(gameObject);
        SetActiveSceneAudio(_sceneAudio);

        //DontDestroyOnLoad(gameObject);
    }

    public void SetActiveSceneAudio(ObjectSounds p_sceneAudio)
    {
        foreach (Sound s in p_sceneAudio._objectSounds)
        {
            bool added = _sceneAudio.AddSoundToList(s);

            if (added)
            {
                _sceneAudio.Play(s);
            }

        }
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