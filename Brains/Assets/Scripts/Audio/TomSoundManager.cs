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
    private AudioSource source;
    public bool doDebugPrint = false;

    private AudioManager m_audioman;
    private EnemyBase m_enemy;
    AudioClip chosen;
    private float defaultMusVol;
    private float defaultHighMusVol;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        m_enemy = GetComponent<EnemyBase>();
        m_audioman = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        defaultMusVol = m_audioman.GetSound("BGMusic").volume;
        defaultHighMusVol = m_audioman.GetSound("BGMusicHigh").volume;
    }

    private void Update()
    {

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
        debugPrint("tom footstep");
    }

    public void detectionEvent()
    {
        source.PlayOneShot(detectionSound, 0.3f);
        debugPrint("well whistle me dixie");
    }

    public void failUIEvent()
    {
        m_audioman.Play("Fail");
        debugPrint("kuhPANG");
    }

    public void musicDucking(float vol, bool returnTo)
    {
        if (returnTo) m_audioman.SetVol("BGMusic", defaultMusVol);
        else m_audioman.SetVol("BGMusic", vol);
    }

    public void dangerMusic(float vol, bool returnTo)
    {
        if (returnTo) m_audioman.SetVol("BGMusicHigh", 0f);
        else m_audioman.SetVol("BGMusicHigh", vol);
    }
    public void swingEvent()
    {
        source.PlayOneShot(swingSound);
        debugPrint("A-SWING");
    }
    public void punchHitEvent()
    {
        source.PlayOneShot(punchHitSound);
        debugPrint("HIT");
    }
}
