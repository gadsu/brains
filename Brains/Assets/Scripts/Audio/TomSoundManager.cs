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

    // Use this for initialization
    void Start () {
        AudioClip chosen;
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        vel = anim.GetFloat("velocity");
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
        //chosen = clips[Mathf.RoundToInt(Random.Range(1, clips.Length))];
        debugPrint("tom footstep");
    }
}
