using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    Animator _anim;
    bool _isBlocked;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _isBlocked = false;
    }

    public void SetAnimationSpeed(float p_speed)
    {
        _anim.SetFloat("Velocity", p_speed);
    }

    public void SetAnimation(string p_name, bool p_blocking, bool p_chasing, NavMeshAgent p_agent, float p_speed, Transform p_target, Vector3 p_direction)
    {
        if (p_blocking)
        {
            BlockingAnimCo(p_name, p_agent);
        }
        else
        {
            if (!_isBlocked)
            {
                p_agent.speed = (p_chasing) ? p_speed * 2f : p_speed;
            }
            else
            {
                transform.LookAt(p_target, p_direction);
            }
        }
    }

    IEnumerator BlockingAnimCo(string p_anim, NavMeshAgent p_agent)
    {
        if (!_isBlocked)
        {
            _isBlocked = true;
            float time = 0;
            AnimationClip[] clips = _anim.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == p_anim)
                {
                    time = clip.length;
                    p_agent.speed = 0;
                    _anim.Play(p_anim);
                }
            }
            yield return new WaitForSeconds(time);
            _anim.SetTrigger("toMove");
            _isBlocked = false;
        }
    }
}