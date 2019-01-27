/* 
 * Objective: Create a modifiable sound list for the game Brains: Graveyard Bound.
 * Original Author: Paul Manley
 * Modified by:
*/
using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Object Sounds")] // Allows for this ScriptableObject to be created by going to the Create -> Asset -> ObjectSounds in the menu. Or right click and got to create in Project Folders.
public class ObjectSounds : ScriptableObject
{
    public List<Sound> _objectSounds; // the list of sounds stored in this ScriptableObject.
    KeyNotFoundException _soundException = new KeyNotFoundException("<color=blue>Sound not found: "); // my Specialied KeyNotFoundException.

    public void InitSounds(GameObject gameObject)
    {// Allows the InitSounds to run and set audio to a new source, attatched to the passed GameObject, if one is not already provided.
        for (int i = 0; i < _objectSounds.Capacity; i++)
        {
            if (_objectSounds[i].source == null)
            {
                _objectSounds[i].source = gameObject.AddComponent<AudioSource>();
                _objectSounds[i].source.clip = _objectSounds[i].clip;
                _objectSounds[i].source.volume = _objectSounds[i].defaultVolume;
                _objectSounds[i].source.pitch = _objectSounds[i].pitch;
                _objectSounds[i].source.loop = _objectSounds[i].loop;
            }
        }
    }

    public void InitSounds(GameObject gameObject, AudioSource _aSource)
    {// Allows the InitSounds to run and set audio to the passed source.
        for (int i = 0; i < _objectSounds.Capacity; i++)
        {
            if (_objectSounds[i].source == null)
            {
                _objectSounds[i].source = _aSource;
                _objectSounds[i].source.clip = _objectSounds[i].clip;
                _objectSounds[i].source.volume = _objectSounds[i].defaultVolume;
                _objectSounds[i].source.pitch = _objectSounds[i].pitch;
                _objectSounds[i].source.loop = _objectSounds[i].loop;
            }
        }
    }

    public Sound GetSound(string p_name)
    {// Gets the sound, from the list, given the passed name.
        Sound _s = null;
        try
        {
            for (int i = 0; i < _objectSounds.Capacity; i++)
            {
                if (_objectSounds[i].name == p_name)
                    _s = _objectSounds[i];
            }

            if(_s == null) throw _soundException;
        }
        catch (KeyNotFoundException k)
        {
            Debug.LogAssertion(k + p_name + "</color>");
        }

        return _s;
    }

    public void Play(Sound s)
    { // plays the sound according to it's settings.
        if (s == null)
            return;

        if (!s.source.isPlaying)
        {
            if (s.oneShot)
                s.source.PlayOneShot(s.clip);
            else
                s.source.Play();
        }
    }

    public void Play(string name)
    { // Plays the sound by first getting it then sending it to the original Play() function.
        Play(GetSound(name));
    }

    public void Play(string name, float p_volume)
    { // gets the sound but then adjusts its volume by the passed volume then plays it.
        Sound l_sound = GetSound(name);
        if (l_sound != null)
        {
            l_sound.source.volume = p_volume;
            l_sound.volume = p_volume;
            Play(l_sound);
        }
    }

    public void Play(string name, float p_volume, float pitch)
    {// Directly modifies the sound based assuming its already running.
        Sound l_sound = GetSound(name);
        l_sound.volume = p_volume;
        l_sound.pitch = pitch;
        l_sound.source.volume = p_volume;
        l_sound.source.pitch = pitch;
    }

    public void SetVolume(string p_name, float p_volume)
    {// Directly modifies the sound volume.
        Sound s = GetSound(p_name);

        if (s != null)
        {
            s.volume = p_volume;
            s.source.volume = s.volume;
        }
    }

    public float GetVolume(string name)
    { // returns the sound volume if the sound exists.
        return (GetSound(name) != null) ? GetSound(name).volume : 0f;
    }

    public void SetPitch(string p_name, float p_pitch)
    { // sets the pitch directly.
        Sound s = GetSound(p_name);
        if (s != null)
        {
            s.pitch = p_pitch;
            s.source.pitch = s.pitch;
        }
    }

    public float GetPitch(string name)
    { // returns the current pitch of the sound.
        return (GetSound(name) != null) ? GetSound(name).pitch : 0f;
    }
}
