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
    private AudioSource _source;
    public bool doDebugPrint = false;
    AudioClip _chosen;

    // Use this for initialization
    void Start()
    {
        _source = GetComponent<AudioSource>();
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
        _chosen = footstepClips[Mathf.RoundToInt(Random.Range(1, footstepClips.Length))];
        _source.PlayOneShot(_chosen);
        debugPrint("spud footstep");
    }
    public void crawlStartEvent()
    {
        _source.PlayOneShot(crawlStart);
        _source.volume = 1;
        debugPrint("spud crawl start");
    }
    public void crawlEndEvent()
    {
        _source.PlayOneShot(crawlEnd);
        _source.volume = 1;
        debugPrint("spud crawl end");
    }
    public void playDeadEvent()
    {
        _source.PlayOneShot(playDead);
        debugPrint("spud playdead test");
    }
    public void crawlEvent()
    {
        _source.PlayOneShot(crawl);
        debugPrint("spud crawl test");
    }
}
