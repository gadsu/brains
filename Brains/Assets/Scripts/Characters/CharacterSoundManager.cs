using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{
    public ObjectSounds sounds;
    public ObjectSounds footSounds;

    private void Awake()
    {
        sounds.InitSounds(gameObject, GetComponent<AudioSource>());
        footSounds.InitSounds(gameObject, GetComponent<AudioSource>());
    }

    public void FootEvent()
    {
        footSounds.Play(footSounds.objectSounds[Mathf.RoundToInt(Random.Range(0, footSounds.objectSounds.Capacity))]);
    }

    public void PlayChSoundEvent(string name, float? volume)
    {
        sounds.Play(name, volume ?? sounds.GetSound(name).volume);
    }
}
