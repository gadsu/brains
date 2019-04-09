/* 
 * Objective: Create a modifiable sound list for the game Brains: Graveyard Bound.
 * Original Author: Paul Manley
 * Modified by:
*/
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Object Sounds")] // Allows for this ScriptableObject to be created by going to the Create -> Asset -> ObjectSounds in the menu. Or right click and got to create in Project Folders.
public class ObjectSounds : ScriptableObject
{
    public List<Sound> objectSounds; // the list of sounds stored in this ScriptableObject.
    readonly KeyNotFoundException _soundException = new KeyNotFoundException("<color=blue>Sound not found: "); // my Specialied KeyNotFoundException.

    public void InitSounds(GameObject gameObject)
    {// Allows the InitSounds to run and set audio to a new source, attatched to the passed GameObject, if one is not already provided.
        for (int i = 0; i < objectSounds.Capacity; i++)
        {
            if (objectSounds[i].source == null)
            {
                objectSounds[i].source = gameObject.AddComponent<AudioSource>();
                objectSounds[i].source.clip = objectSounds[i].clip;
                objectSounds[i].source.volume = objectSounds[i].defaultVolume;
                objectSounds[i].source.pitch = objectSounds[i].pitch;
                objectSounds[i].source.loop = objectSounds[i].loop;
                //TODO FIX 3D SOUND SETTINGS
                //objectSounds[i].source.spatialBlend = 1;
            }
        }
    }

    public void InitSounds(GameObject gameObject, AudioSource _aSource)
    { // Allows the InitSounds to run and set audio to the passed source.
        for (int i = 0; i < objectSounds.Capacity; i++)
        {
            if (objectSounds[i].source == null)
            {
                objectSounds[i].source = _aSource;
                objectSounds[i].source.clip = objectSounds[i].clip;
                objectSounds[i].source.volume = objectSounds[i].defaultVolume;
                objectSounds[i].source.pitch = objectSounds[i].pitch;
                objectSounds[i].source.loop = objectSounds[i].loop;
            }
        }
    }

    public Sound GetSound(string p_name)
    {// Gets the sound, from the list, given the passed name.
        Sound _s = null;
        try
        {
            for (int i = 0; i < objectSounds.Capacity; i++)
            {
                if (objectSounds[i].name == p_name)
                    _s = objectSounds[i];
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
        //Debug.Log(name);
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

    /////////////////////////////////////////////////////// MISTAH PAUL
    /// I added this to set the vol of all the sounds in the objectSounds (for the footstep sounds).
    public void SetVolume(float p_volume)
    {// Directly modifies the sound volume.
        for (int i = 0; i < objectSounds.Capacity; i++)
        {
            objectSounds[i].source.volume = p_volume;
        }
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

    //public bool AddSoundToList(Sound pSound)
    //{
    //    bool soundExists = false;
    //    foreach (Sound s in objectSounds)
    //    {
    //        if (s.source.clip.name == pSound.source.clip.name)
    //        {
    //            s.name = pSound.name;
    //            s.volume = pSound.volume;
    //            s.volumeScale = pSound.volumeScale;
    //            s.source.volume = s.volume * s.volumeScale;
    //            Play(s);
    //            soundExists = true;
    //        }
    //    }
    //    if (!soundExists)
    //    {
    //        objectSounds.Add(pSound);
    //    }

    //    return !soundExists;
    //}

    public void SetToDefaultVolume(string pName)
    {
        Sound l_sound = GetSound(pName);

        l_sound.source.volume = l_sound.defaultVolume;
    }
}
