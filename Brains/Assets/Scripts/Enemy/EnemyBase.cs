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
    public float moveSpeedStart = 3f;
    bool chasing;

    private void Awake()
    {
        mAEnemy_pathing = GetComponent<PathTo>();
        mAEnemy_detecting = GetComponent<DetectPlayer>();
        m_agent = GetComponent<NavMeshAgent>();
        m_target = GameObject.Find("Spud").GetComponent<Transform>();
        _animHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (mAEnemy_detecting.IsInView(m_target.position))
        {
            Enemy_Detection = DetectionLevel.Detecting;
            mAEnemy_detecting.UpdateRayToPlayer(m_target.position);

            if (mAEnemy_detecting.IsVisible(m_target.position))
            {
                chasing = true;
                Enemy_Awareness = AwarenessLevel.Aware;
                mAEnemy_pathing.UpdateDestination(chasing);
                if (chasing) _animHandler.SetAnimation("A_TomSurprise", true, chasing, m_agent, moveSpeedStart, m_target, Vector3.up);
            }
            else
            {
                if (Enemy_Awareness == AwarenessLevel.Aware) Enemy_Awareness = AwarenessLevel.Losing;
                else Enemy_Awareness = AwarenessLevel.Unaware;
            }
        }
        else
        {
            chasing = false;
            mAEnemy_pathing.UpdateDestination(chasing);
            if (chasing) _animHandler.SetAnimation("A_TomSurprise", true, chasing, m_agent, moveSpeedStart, m_target, Vector3.up);

            if (Enemy_Detection == DetectionLevel.Detecting) Enemy_Detection = DetectionLevel.Searching;
            else if (Enemy_Detection == DetectionLevel.Searching) Enemy_Detection = DetectionLevel.Losing;
            else Enemy_Detection = DetectionLevel.Unseen;
        }

        mAEnemy_detecting.UpdatingDetectionAmount(mAEnemy_sightValue, mAEnemy_hearValue, m_target, (int)Enemy_Detection, (int)Enemy_Awareness);
    }

    private void FixedUpdate()
    {

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
