/* 
 * Objective: Create a modifiable sound for the game Brains: Graveyard Bound.
 * Original Author: Paul Manley
 * Modified by:
*/
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // Stores the name which also overrides the name used in the inspector.

    public AudioClip clip; // Stores the Audio clip that we want to use here.

    [Range(0f, 1f)]
    public float volume; // stores the current run-time modified volume.
    [Range(.1f, 3f)]
    public float pitch; // stores the pitch.

    [Range(0f, 1f)]
    public float defaultVolume, volumeScale; // stores the default volume that will be used at beginning of scene and the volumeScale which will be modified at run-time.

    public bool loop, oneShot; // allows the developer to set the whether or not the sound loops, or if it is a OneShot which can be consolidated if is necessary.

    [HideInInspector] // Doesn't really do anything I think, don't remember why I have it.
    public AudioSource source; 
}
