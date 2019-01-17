using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Object Sounds")]
public class ObjectSounds : ScriptableObject
{
    public List<Sound> _objectSounds;
    KeyNotFoundException _soundException = new KeyNotFoundException("<color=blue>Sound not found: ");

    public void InitSounds(GameObject gameObject)
    {
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
    {
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
    {
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
    {
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
    {
        Play(GetSound(name));
    }

    public void Play(string name, float p_volume)
    {
        Sound l_sound = GetSound(name);
        if (l_sound != null)
        {
            l_sound.source.volume = p_volume;
            l_sound.volume = p_volume;
            Play(l_sound);
        }
    }

    public void Play(string name, float p_volume, float pitch)
    {
        Sound l_sound = GetSound(name);
        l_sound.volume = p_volume;
        l_sound.pitch = pitch;
        l_sound.source.volume = p_volume;
        l_sound.source.pitch = pitch;
    }

    public void SetVolume(string p_name, float p_volume)
    {
        Sound s = GetSound(p_name);

        if (s != null)
        {
            s.volume = p_volume;
            s.source.volume = s.volume;
        }
    }

    public float GetVolume(string name)
    {
        return (GetSound(name) != null) ? GetSound(name).volume : 0f;
    }

    public void SetPitch(string p_name, float p_pitch)
    {
        Sound s = GetSound(p_name);
        if (s != null)
        {
            s.pitch = p_pitch;
            s.source.pitch = s.pitch;
        }
    }

    public float GetPitch(string name)
    {
        return (GetSound(name) != null) ? GetSound(name).pitch : 0f;
    }
}
