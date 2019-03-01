using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TomSoundManager : MonoBehaviour
{

    public AudioClip[] footstepClips;
    public AudioClip detectionSound;
    public AudioClip swingSound;
    public AudioClip punchHitSound;
    public bool doDebugPrint = false;
    private AudioSource _source;

    private AudioManager _audioman;
    private EnemyBase _enemy;
    private float _defaultMusVol = 1f;

    // Use this for initialization
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _enemy = GetComponent<EnemyBase>();
        _audioman = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        //defaultMusVol = m_audioman.GetSound("BGMusic").volume;
        //defaultHighMusVol = m_audioman.GetSound("BGMusicHigh").volume;
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
        _source.PlayOneShot(footstepClips[Mathf.RoundToInt(Random.Range(1, footstepClips.Length))]);
        debugPrint("tom footstep");
    }

    public void detectionEvent()
    {
        _source.PlayOneShot(detectionSound, 0.3f);
        debugPrint("well whistle me dixie");
    }

    public void failUIEvent()
    {
        _audioman.Play("Fail");
        debugPrint("kuhPANG");
    }

    public void musicDucking(float vol, bool returnTo)
    {
        if (returnTo) _audioman.SetVol("BGMusic", _defaultMusVol);
        else _audioman.SetVol("BGMusic", vol);
    }

    public void dangerMusic(float vol, bool returnTo)
    {
        if (returnTo) _audioman.SetVol("BGMusicHigh", 0f);
        else _audioman.SetVol("BGMusicHigh", vol);
    }
    public void swingEvent()
    {
        _source.PlayOneShot(swingSound);
        debugPrint("A-SWING");
    }
    public void punchHitEvent()
    {
        _source.PlayOneShot(punchHitSound);
        debugPrint("HIT");
    }
}
