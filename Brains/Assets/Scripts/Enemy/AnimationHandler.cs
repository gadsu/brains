using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    Animator _anim;
    public bool isBlocked;
    Vector3 whyareunityvectorssostupid;
    Vector3 temp;
    GameObject spud;
    public float lookDownValue = -1f;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        isBlocked = false;
        spud = GameObject.Find("Spud");
    }

    private void LateUpdate()
    {
        if(isBlocked)
        {
            temp = spud.transform.position;
            if (spud.GetComponent<Player>().MState == ACharacter.MovementState.Crawling || spud.GetComponent<Player>().playDead == 1)
            {
                whyareunityvectorssostupid = new Vector3();
                whyareunityvectorssostupid.x = temp.x;
                whyareunityvectorssostupid.y = temp.y - lookDownValue;
                whyareunityvectorssostupid.z = temp.z;
                transform.LookAt(whyareunityvectorssostupid, Vector3.up);
            }
            else
            {
                transform.LookAt(temp, Vector3.up);
            }
        }
    }

    public void SetAnimationSpeed(float p_speed)
    {
        _anim.SetFloat("Velocity", p_speed);
    }

    public void SetAnimation(string pName, bool pBlocking, int detectionState, NavMeshAgent pAgent, float pSpeed)
    {
        if (isBlocked == false)
        {
            pAgent.speed = (detectionState == 4) ? pSpeed * 2f : pSpeed;

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
                pAgent.speed = 0.5f;
                _anim.Play(pAnim);
            }
        }
        yield return new WaitForSeconds(time);
        isBlocked = false;
        pAgent.isStopped = false;
    }
}