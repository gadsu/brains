using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public ObjectSounds sceneAudio;

    //public static AudioManager instance;

	// Use this for initialization
	void Awake () {

        sceneAudio.InitSounds(gameObject);
        SetActiveSceneAudio(sceneAudio);
    }

    public void SetActiveSceneAudio(ObjectSounds p_sceneAudio)
    {
        foreach (Sound s in p_sceneAudio.objectSounds)
        {
            bool added = sceneAudio.AddSoundToList(s);

            if (added)
            {
                sceneAudio.Play(s);
            }
        }
    }

    public void Play(string name)
    {
        if (PauseMenu.GamePaused) sceneAudio.SetPitch(name, sceneAudio.GetPitch(name) * 3f);

        sceneAudio.Play(name);
    }

    public void SetVol(string name, float vol)
    {
        vol = Mathf.Clamp(vol, 0, 1f);
        sceneAudio.SetVolume(name, vol);
    }

    public Sound GetSound(string name)
    {
        return sceneAudio.GetSound(name);
    }
}