using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TomSoundManager : MonoBehaviour {

    public AudioClip[] footstepClips;
    private AudioSource source;
    public bool doDebugPrint = false;
    private Animator anim;
    private float vel;
    AudioClip chosen;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        vel = anim.GetFloat("Velocity");
    }

    void debugPrint(string msg)
    {
        if (doDebugPrint)
        {
            Debug.Log(msg);
        }
    }

    public void footEvent()
    {
        source.volume = vel;
        chosen = footstepClips[Mathf.RoundToInt(Random.Range(1, footstepClips.Length))];
        source.PlayOneShot(chosen);
        debugPrint("tom footstep");
    }
}
