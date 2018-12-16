using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    protected Dictionary<string, Sound> audioDictionary;
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

        audioDictionary = new Dictionary<string, Sound>();

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < _sceneAudio._objectSounds.Capacity; i++)
        {
            if (!audioDictionary.ContainsKey(_sceneAudio._objectSounds[i].name))
            {
                audioDictionary.Add(_sceneAudio._objectSounds[i].name, _sceneAudio._objectSounds[i]);
            }
            else { Debug.Log("<color=yellow>Key already exists!</color>"); }
        }

		foreach (Sound s in audioDictionary.Values)
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
        Play("Background Music");
        Play("Background Music High");
        SetVol("Background Music High", 0);
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
                SetVol("Main Music", 0.2f);
                SetVol("Background Music", 0f);
                SetVol("Background Music High", 0f);
                break;
            default:
                SetVol("Main Music", 0f);
                SetVol("Background Music", 0.1f);
                break;
        }
    }

    public void Play(string name)
    {
        Sound s = new Sound();
        if (audioDictionary.TryGetValue(name, out s))
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
        if (audioDictionary.TryGetValue(name, out s))
        {
            s.source.volume = vol;
        }
    }

    public Sound GetSound(string name)
    {
        Sound s = new Sound();
        if (audioDictionary.TryGetValue(name, out s))
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