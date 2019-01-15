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

    string animationToPlay = "";
    public float moveSpeedStart = 3f;
    bool chasing = false, surpised = false, touched = false, blockAnimation = false;
    private float lastTouchedTime = 0f;

    private void Awake()
    {
        mAEnemy_pathing = GetComponent<PathTo>();
        mAEnemy_detecting = GetComponent<DetectPlayer>();
        m_sound = GetComponent<TomSoundManager>();

        m_target = GameObject.Find("Spud").GetComponent<Transform>();
        m_agent = GetComponent<NavMeshAgent>();
        _animHandler = GetComponent<AnimationHandler>();
    }

    private void Update()
    {
        if ((mAEnemy_detecting.IsInView(m_target.position) || touched) && m_target.GetComponent<Player>().m_playDead == 0)
        {
            Enemy_Detection = DetectionLevel.Detecting;

            mAEnemy_detecting.UpdateRayToPlayer(m_target.position);
            if (mAEnemy_detecting.IsVisible(m_target.position) || touched)
            {
                Enemy_Awareness = AwarenessLevel.Aware;
                if (!chasing)
                {
                    chasing = true;
                    surpised = true;
                    animationToPlay = "A_TomSurprise";
                    blockAnimation = true;
                }
                else
                {
                    if (m_agent.remainingDistance < 5f)
                    {
                        animationToPlay = "A_TomAttack";
                        blockAnimation = true;
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

        m_agent.SetDestination(mAEnemy_pathing.UpdateDestination(chasing, m_agent.destination, m_agent.remainingDistance));

        //mAEnemy_detecting.UpdatingDetectionAmount(mAEnemy_sightValue, mAEnemy_hearValue, m_target, (int)Enemy_Detection, (int)Enemy_Awareness);        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && Enemy_Awareness == AwarenessLevel.Unaware)
        {
            lastTouchedTime = Time.time;
            touched = true;
        }
    }

    private void LateUpdate()
    {
        _animHandler.SetAnimation(animationToPlay, blockAnimation, chasing, m_agent, moveSpeedStart, m_target, Vector3.up);
        _animHandler.SetAnimationSpeed(m_agent.velocity.magnitude);

        touched = (Mathf.Abs(lastTouchedTime - Time.time) > 3f) ? false : touched;

        animationToPlay = "A_TomWalk";
        blockAnimation = false;
        surpised = false;
    }
}
