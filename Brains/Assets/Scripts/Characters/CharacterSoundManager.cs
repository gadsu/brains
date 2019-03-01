using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{
    public ObjectSounds _sounds;
    public ObjectSounds _footSounds;

    private void Awake()
    {
        _sounds.InitSounds(gameObject, GetComponent<AudioSource>());
        _footSounds.InitSounds(gameObject, GetComponent<AudioSource>());
    }

    public void FootEvent()
    {
        _footSounds.Play(_footSounds.objectSounds[Mathf.RoundToInt(Random.Range(0, _footSounds.objectSounds.Capacity))]);
    }

    public void PlayChSoundEvent(string name, float? volume)
    {
        _sounds.Play(name, volume ?? _sounds.GetSound(name).volume);
    }
}
