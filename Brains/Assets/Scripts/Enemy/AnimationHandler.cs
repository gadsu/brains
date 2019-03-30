using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    Animator _anim;
    public bool isBlocked;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        isBlocked = false;
    }

    public void SetAnimationSpeed(float p_speed)
    {
        _anim.SetFloat("Velocity", p_speed);
    }

    public void SetAnimation(string pName, bool pBlocking, bool pChasing, NavMeshAgent pAgent, float pSpeed)
    {
        if (isBlocked == false)
        {
            pAgent.speed = (pChasing) ? pSpeed * 2f : pSpeed;

            if (pBlocking)
            {
                StartCoroutine(BlockingAnimCo(pName, pAgent));
            }
            else
            {
                _anim.Play(pName);
            }
        }
    }

    IEnumerator BlockingAnimCo(string pAnim, NavMeshAgent pAgent)
    {
        isBlocked = true;
        float time = 0;
            
        // Determine legnth of the animationClip
        AnimationClip[] clips = _anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == pAnim)
            {
                time = clip.length+0.05f;

                Debug.Log("Clip time: <color=yellow>" + clip.length + "</color> Clip name: <color=blue>" + clip.name + "</color>");
                pAgent.speed = 0;
                _anim.Play(pAnim);
            }
        }
        yield return new WaitForSeconds(time);
        isBlocked = false;
        pAgent.isStopped = false;
    }
}