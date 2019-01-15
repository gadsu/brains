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
        _footSounds.Play(_footSounds._objectSounds[Mathf.RoundToInt(Random.Range(1, _footSounds._objectSounds.Capacity))]);
    }

    public void PlayChSoundEvent(string name, float? volume)
    {
        if (volume != null)
        {
            _sounds.Play(name, (float)volume);
            return;
        }
    }
}
