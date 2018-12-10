using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AnimationHandler))]
public class EnemyBase : AEnemy
{
    Transform m_target;
    NavMeshAgent m_agent;
    AnimationHandler _animHandler;
    TomSoundManager m_sound;
    GameObject m_spud;

    public float moveSpeedStart = 3f;
    bool chasing;
    bool surpised;
    bool touched;
    private float lastTouchedTime = 0f;
    //private float musicDuckLevel = 0f;

    private void Awake()
    {
        mAEnemy_pathing = GetComponent<PathTo>();
        mAEnemy_detecting = GetComponent<DetectPlayer>();
        m_sound = GetComponent<TomSoundManager>();

        m_target = GameObject.Find("Spud").GetComponent<Transform>();
        m_agent = GetComponent<NavMeshAgent>();
        _animHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        chasing = false;
        surpised = false;
        touched = false;
    }

    /*private void FixUpdate()
    {
        switch(Enemy_Awareness) {
        case AwarenessLevel.Aware: {
            
            break;
            }
        case AwarenessLevel.Unaware: {
            
            break;
            }
        }
    }*/

    private void Update()
    {
        if (mAEnemy_detecting.IsInView(m_target.position) || touched)
        {
            Enemy_Detection = DetectionLevel.Detecting;
            if (Enemy_Awareness != AwarenessLevel.Aware) { m_sound.musicDucking(0f, false); }
            else { m_sound.musicDucking(1f, true); }

            mAEnemy_detecting.UpdateRayToPlayer(m_target.position);
            if (mAEnemy_detecting.IsVisible(m_target.position) || touched)
            {
                Enemy_Awareness = AwarenessLevel.Aware;
                if (!chasing)
                {
                    chasing = true;
                    surpised = true;
                }
                else
                {
                    if (touched == true)
                    {
                        Debug.Log("PUNCH");
                    }
                }
            }
            else
            {
                if (Enemy_Awareness == AwarenessLevel.Aware) Enemy_Awareness = AwarenessLevel.Losing;
                else Enemy_Awareness = AwarenessLevel.Unaware;

                Enemy_Awareness = AwarenessLevel.Unaware;

                chasing = false;
            }
        }
        else
        {
            chasing = false;
            Enemy_Awareness = AwarenessLevel.Unaware;

            if (Enemy_Detection == DetectionLevel.Detecting) Enemy_Detection = DetectionLevel.Searching;
            else if (Enemy_Detection == DetectionLevel.Searching) Enemy_Detection = DetectionLevel.Losing;
            else Enemy_Detection = DetectionLevel.Unseen;
        }

        if (chasing)
        {
            m_sound.dangerMusic(1f, true);

            if (surpised)
            {
                m_sound.detectionEvent();
                //m_sound.dangerMusic(1f,false);
                _animHandler.SetAnimation("A_TomSurprise", true, chasing, m_agent, moveSpeedStart, m_target, Vector3.up);
            }
            else
            {
                _animHandler.SetAnimation("A_TomWalk", false, chasing, m_agent, moveSpeedStart, m_target, Vector3.up);
            }
        }
        else
        {
            _animHandler.SetAnimation("A_TomWalk", false, chasing, m_agent, moveSpeedStart, m_target, Vector3.up);
        }

        m_agent.SetDestination(mAEnemy_pathing.UpdateDestination(chasing, m_agent.destination, m_agent.remainingDistance));
        if (m_target.position == m_agent.destination) Debug.Log(true);

        //mAEnemy_detecting.UpdatingDetectionAmount(mAEnemy_sightValue, mAEnemy_hearValue, m_target, (int)Enemy_Detection, (int)Enemy_Awareness);
        surpised = false;
        //Debug.Log(Mathf.Abs(lastTouchedTime - Time.time));
        if (Mathf.Abs(lastTouchedTime - Time.time) > 3f)
        {
            touched = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && Enemy_Awareness == AwarenessLevel.Unaware)
        {
            lastTouchedTime = Time.time;
            touched = true;
        }
    }

    private void LateUpdate()
    {
        _animHandler.SetAnimationSpeed(m_agent.velocity.magnitude);
    }
}
/*
if (m_DetectPlayer.IsInView(m_target.position))
{
    if (m_DetectPlayer.IsVisible(m_target.position) && !isChasing)
    {
        isChasing = true;
        m_pathTo.SetVisible(true);
        FindObjectOfType<AudioManager>().Play("Dixie");
        FindObjectOfType<AudioManager>().setVol("BGMusicHigh", 0.75f);
        blockingAnim("A_TomSurprise");
        Debug.Log("<color=yellow>Tom Path Set To Player! </color>");
    }
}
else isChasing = false;

if (!isBlocked)
{
    m_agent.speed = (isChasing) ? moveSpeedStart * 2f : moveSpeedStart;
}
else if (isBlocked)
{
    transform.LookAt(m_target, Vector3.up);
}

public void blockingAnim(string anim)
{
    StartCoroutine(blockingAnimCo(anim));
}
IEnumerator blockingAnimCo(string anim)
{
    if (!isBlocked)
    {
        isBlocked = true;
        Animator m_animator = GetComponent<Animator>();
        float time = 0;
        AnimationClip[] clips = m_animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == anim)
            {
                time = clip.length;
                m_agent.speed = 0;
                m_animator.Play(anim);
            }
        }
        yield return new WaitForSeconds(time);
        m_animator.SetTrigger("toMove");
        isBlocked = false;
    }
}*/
