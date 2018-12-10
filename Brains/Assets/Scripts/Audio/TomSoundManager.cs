using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TomSoundManager : MonoBehaviour
{

    public AudioClip[] footstepClips;
    public AudioClip detectionSound;
    private AudioSource source;
    public bool doDebugPrint = false;


    private Animator anim;
    private AudioManager m_audioman;
    private float vel;
    private EnemyBase m_enemy;
    AudioClip chosen;
    private float defaultMusVol;
    private float defaultHighMusVol;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        m_enemy = GetComponent<EnemyBase>();
        m_audioman = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        defaultMusVol = m_audioman.getSound("BGMusic").volume;
        defaultHighMusVol = m_audioman.getSound("BGMusicHigh").volume;
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
        source.PlayOneShot(detectionSound, 0.5f);
        debugPrint("well whistle me dixie");
    }

    public void failUIEvent()
    {
        m_audioman.Play("Fail");
        debugPrint("kuhPANG");
    }

    public void musicDucking(float vol, bool returnTo)
    {
        if (returnTo) m_audioman.setVol("BGMusic", defaultMusVol);
        else m_audioman.setVol("BGMusic", vol);
    }

    public void dangerMusic(float vol, bool returnTo)
    {
        if (returnTo) m_audioman.setVol("BGMusicHigh", 0f);
        else m_audioman.setVol("BGMusicHigh", vol);
    }
}
