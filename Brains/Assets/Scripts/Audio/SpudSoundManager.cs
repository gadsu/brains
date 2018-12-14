using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpudSoundManager : MonoBehaviour
{
    public AudioClip[] footstepClips;
    public AudioClip crawlStart;
    public AudioClip crawl;
    public AudioClip crawlEnd;
    public AudioClip playDead;
    private AudioSource source;
    public bool doDebugPrint = false;
    AudioClip chosen;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
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
        chosen = footstepClips[Mathf.RoundToInt(Random.Range(1, footstepClips.Length))];
        source.PlayOneShot(chosen);
        debugPrint("spud footstep");
    }
    public void crawlStartEvent()
    {
        source.PlayOneShot(crawlStart);
        source.volume = 1;
        debugPrint("spud crawl start");
    }
    public void crawlEndEvent()
    {
        source.PlayOneShot(crawlEnd);
        source.volume = 1;
        debugPrint("spud crawl end");
    }
    public void playDeadEvent()
    {
        source.PlayOneShot(playDead);
        debugPrint("spud playdead test");
    }
    public void crawlEvent()
    {
        source.PlayOneShot(crawl);
        debugPrint("spud crawl test");
    }
}
