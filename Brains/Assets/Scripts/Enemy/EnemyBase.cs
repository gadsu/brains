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
    public float moveSpeedStart;
    private float moveSpeed;
    private bool isChasing = false;
    private bool isBlocked = false;

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
        isChasing = false;
	}

	private void Update()
	{
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

        if(isChasing && !isBlocked)
        {
            m_agent.speed = moveSpeedStart * 1.5f;
        }
        else if(!isChasing && !isBlocked)
        {
            m_agent.speed = moveSpeedStart;
        }
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
            //Debug.Log(clips[0].name);
            m_animator.SetTrigger("toMove");
            isBlocked = false;
        }
    }
}
