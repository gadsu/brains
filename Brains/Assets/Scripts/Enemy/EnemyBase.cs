using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour 
{
	PathTo m_pathTo;
	DetectPlayer m_DetectPlayer;
	Transform m_target;
    NavMeshAgent m_agent;
    public float moveSpeedStart = 3f;
    private float moveSpeed;

	private void Awake()
	{
		m_pathTo = GetComponent<PathTo>();
		m_DetectPlayer = GetComponent<DetectPlayer>();
        m_agent = GetComponent<NavMeshAgent>();
        moveSpeed = moveSpeedStart;
    }

	private void Start()
	{
		m_target = GameObject.Find("Spud").GetComponent<Transform>();
	}

	private void Update()
	{
        if (m_DetectPlayer.IsInView(m_target.position))
        {
            m_DetectPlayer.UpdateRayToPlayer(m_target.position);
            if (m_DetectPlayer.IsVisible(m_target.position))
            {
                Debug.Log(true);
            }
        }
    }
}

/*if (m_DetectPlayer.IsInView(m_target.position))
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
}*/
/*
public void blockingAnim(string anim)
{
    StartCoroutine(blockingAnimCo(anim));
}*/
/*IEnumerator blockingAnimCo(string anim)
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
